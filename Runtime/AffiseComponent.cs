#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Converter;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Executors;
using AffiseAttributionLib.Init;
using AffiseAttributionLib.Logs;
using AffiseAttributionLib.Network;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.AffiseParameters.Factory;
using AffiseAttributionLib.AffiseParameters.Providers;
using AffiseAttributionLib.Debugger.Network;
using AffiseAttributionLib.Debugger.Validate;
using AffiseAttributionLib.Deeplink;
using AffiseAttributionLib.Modules;
using AffiseAttributionLib.Referrer;
using AffiseAttributionLib.Session;
using AffiseAttributionLib.Storages;
using AffiseAttributionLib.Usecase;
using AffiseAttributionLib.Utils;

namespace AffiseAttributionLib
{
    internal class AffiseComponent : IAffiseApi
    {
        public FirstAppOpenUseCase FirstAppOpenUseCase { get; }
        
        public ISetPropertiesWhenAppInitializedUseCase SetPropertiesWhenInitUseCase { get; }

        public EventsManager EventsManager { get; }

        public IStoreEventUseCase StoreEventUseCase { get; }

        public IInitPropertiesStorage InitPropertiesStorage { get; }

        public IDeeplinkManager DeeplinkManager { get; }

        public PostBackModelFactory PostBackModelFactory { get; }

        public RetrieveReferrerOnServerUseCase RetrieveReferrerOnServerUseCase { get; }
        
        public AffiseModuleManager ModuleManager { get; }
        
        public IDebugNetworkUseCase DebugNetworkUseCase { get; }
        
        public IDebugValidateUseCase DebugValidateUseCase { get; }

        public IPushTokenUseCase PushTokenUseCase { get; }

        public IImmediateSendToServerUseCase ImmediateSendToServerUseCase { get; }

        private readonly ConverterToBase64 _converterToBase64;
        
        private readonly ILogsManager _logsManager;
        
        private readonly ILogsRepository _logsRepository;
        
        private readonly ILogsStorage _logStorage;
        
        private readonly IStoreLogsUseCase _storeLogsUseCase;

        private readonly IEventsStorage _eventsStorage;

        private readonly IEventsRepository _eventsRepository;

        private readonly ICloudRepository _cloudRepository;

        private readonly ISendDataToServerUseCase _sendDataToServerUseCase;

        private readonly ICurrentActiveActivityCountProvider _activityCountProvider;
        
        private readonly ISessionManager _sessionManager;
        
        private readonly IActivityActionsManager _activityActionsManager;
        
        private readonly IDeeplinkClickRepository _isDeeplinkClickRepository;
        
        private readonly IIsFirstForUserStorage _isFirstForUserStorage;
        
        private readonly IIsFirstForUserUseCase _isFirstForUserUseCase;
        
        private readonly IHttpClient _httpClient;
        
        private readonly ProvidersToJsonStringConverter _providersToJsonStringConverter;

        private readonly PostBackModelToJsonStringConverter _postBackModelToJsonStringConverter;
        
        private readonly EventToSerializedEventConverter _eventToSerializedEventConverter;
        
        private readonly IIndexUseCase _indexUseCase;

        private readonly bool _isReady = false;
        
        public AffiseComponent(AffiseInitProperties initProperties)
        {
            _converterToBase64 = new ConverterToBase64();
            _providersToJsonStringConverter = new ProvidersToJsonStringConverter();

            _logStorage = new LogsStorageImpl();
            _logsRepository = new LogsRepositoryImpl(_converterToBase64, new LogToSerializedLogConverter(), _logStorage);
            _storeLogsUseCase = new StoreLogsUseCaseImpl(_logsRepository);
            _logsManager = new LogsManagerImpl(_storeLogsUseCase);
            _httpClient = new HttpClientImpl();
            
            _activityActionsManager = new ActivityActionsManagerImpl();
            _activityCountProvider = new CurrentActiveActivityCountProviderImpl(_activityActionsManager);
            _sessionManager = new SessionManagerImpl(_activityCountProvider);

            _indexUseCase = new IndexUseCaseImpl();
            _postBackModelToJsonStringConverter = new PostBackModelToJsonStringConverter(
                indexUseCase: _indexUseCase
            );

            FirstAppOpenUseCase = new FirstAppOpenUseCase(_activityCountProvider);
            InitPropertiesStorage = new InitPropertiesStorageImpl();
            SetPropertiesWhenInitUseCase = new SetPropertiesWhenAppInitializedUseCaseImpl(InitPropertiesStorage);

            _isDeeplinkClickRepository = new DeeplinkClickRepositoryImpl();
            
            DeeplinkManager = new DeeplinkManagerImpl(
                deeplinkClickRepository: _isDeeplinkClickRepository,
                activityActionsManager: _activityActionsManager
            );
            DeeplinkManager.Init();
            
            FirstAppOpenUseCase.OnAppCreated();
            SetPropertiesWhenInitUseCase.Init(initProperties);
            _sessionManager.Init();
            
            _eventToSerializedEventConverter = new EventToSerializedEventConverter(
                indexUseCase: _indexUseCase
            );

            _eventsStorage = new EventsStorageImpl(_logsManager);
            _eventsRepository = new EventsRepositoryImpl(
                converterToBase64: _converterToBase64,
                eventToSerializedEventConverter: _eventToSerializedEventConverter,
                eventsStorage: _eventsStorage
            );
            
            PushTokenUseCase = new PushTokenUseCase();

            PostBackModelFactory = new PropertiesProviderFactory(
                firstAppOpenUseCase: FirstAppOpenUseCase,
                sessionManager: _sessionManager,
                initPropertiesStorage: InitPropertiesStorage,
                stringToSHA256Converter: new StringToSHA256Converter(),
                stringToMd5Converter: new StringToMD5Converter(),
                deeplinkClickRepository: _isDeeplinkClickRepository,
                pushTokenUseCase: PushTokenUseCase
            ).Create();

            ModuleManager = new AffiseModuleManager(
                logsManager: _logsManager,
                postBackModelFactory: PostBackModelFactory,
                initProperties: initProperties
            );
            ModuleManager.Init(
                new List<object>
                {
                    _httpClient,
                    _providersToJsonStringConverter
                }
            );

            RetrieveReferrerOnServerUseCase = new RetrieveReferrerOnServerUseCase(
                moduleManager: ModuleManager
            );
            
            DebugNetworkUseCase = new DebugNetworkUseCaseImpl(
                initProperties: initProperties,
                httpClient: _httpClient
            );
            DebugValidateUseCase = new DebugValidateUseCaseImpl(
                initProperties: initProperties,
                postBackModelFactory: PostBackModelFactory,
                logsManager: _logsManager,
                httpClient:_httpClient,
                executorServiceProvider: new ExecutorServiceProviderImpl(),
                converter: _providersToJsonStringConverter
            );
            
            _cloudRepository = new CloudRepositoryImpl(
                executorServiceProvider: new ExecutorServiceProviderImpl(),
                httpClient: _httpClient,
                userAgentProvider: PostBackModelFactory.GetProvider<UserAgentProvider>(),
                postBackModelToJsonStringConverter: _postBackModelToJsonStringConverter
            );
            
            _sendDataToServerUseCase = new SendDataToServerUseCaseImpl(
                executorServiceProvider: new ExecutorServiceProviderImpl(),
                postBackModelFactory: PostBackModelFactory,
                cloudRepository: _cloudRepository,
                eventsRepository: _eventsRepository,
                logsRepository: _logsRepository,
                logsManager: _logsManager,
                firstAppOpenUseCase: FirstAppOpenUseCase
            );

            ImmediateSendToServerUseCase = new ImmediateSendToServerUseCaseImpl(
                executorServiceProvider: new ExecutorServiceProviderImpl(),
                cloudRepository: _cloudRepository,
                postBackModelFactory: PostBackModelFactory,
                eventToSerializedEventConverter: _eventToSerializedEventConverter,
                logsManager: _logsManager
            );

            _isFirstForUserStorage = new IsFirstForUserStorageImpl();
            
            _isFirstForUserUseCase = new IsFirstForUserUseCaseImpl(_isFirstForUserStorage);

            EventsManager = new EventsManager(
                executorServiceProvider: new ExecutorServiceProviderImpl(),
                sendDataToServerUseCase: _sendDataToServerUseCase
            );
            EventsManager.Init();

            StoreEventUseCase = new StoreEventUseCaseImpl(
                eventsRepository: _eventsRepository,
                eventsManager: EventsManager,
                isFirstForUserUseCase: _isFirstForUserUseCase
            );

            _isReady = true;
        }

        public bool IsInitialized()
        {
            return _isReady;
        }
    }
}