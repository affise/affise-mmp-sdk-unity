using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters.Factory;
using AffiseAttributionLib.AffiseParameters.Providers;
using AffiseAttributionLib.Converter;
using AffiseAttributionLib.Deeplink;
using AffiseAttributionLib.Init;
using AffiseAttributionLib.Session;
using AffiseAttributionLib.Usecase;

namespace AffiseAttributionLib.AffiseParameters.Base
{
    internal class PropertiesProviderFactory
    {
        private readonly FirstAppOpenUseCase _firstAppOpenUseCase;
        private readonly ISessionManager _sessionManager;
        private readonly IInitPropertiesStorage _initPropertiesStorage;
        private readonly IConverter<string, string> _stringToSHA256Converter;
        private readonly IConverter<string, string> _stringToMd5Converter;
        private readonly IDeeplinkClickRepository _deeplinkClickRepository;
        private readonly IPushTokenUseCase _pushTokenUseCase;
        private readonly IAppUUIDs _appUuids;

        public PropertiesProviderFactory(
            FirstAppOpenUseCase firstAppOpenUseCase,
            ISessionManager sessionManager,
            IInitPropertiesStorage initPropertiesStorage,
            IConverter<string, string> stringToSHA256Converter,
            IConverter<string, string> stringToMd5Converter,
            IDeeplinkClickRepository deeplinkClickRepository, 
            IPushTokenUseCase pushTokenUseCase,
            IAppUUIDs appUuids
        )
        {
            _firstAppOpenUseCase = firstAppOpenUseCase;
            _sessionManager = sessionManager;
            _initPropertiesStorage = initPropertiesStorage;
            _stringToSHA256Converter = stringToSHA256Converter;
            _stringToMd5Converter = stringToMd5Converter;
            _deeplinkClickRepository = deeplinkClickRepository;
            _pushTokenUseCase = pushTokenUseCase;
            _appUuids = appUuids;
        }

        public PostBackModelFactory Create()
        {
            var createdTimeProvider = new CreatedTimeProvider();
            var firstOpenTimeProvider = new FirstOpenTimeProvider(_firstAppOpenUseCase);
            var lastSessionTimeProvider = new LastSessionTimeProvider(_sessionManager);
            return new PostBackModelFactory(
                providers: new List<Provider>
                {
                    new UuidProvider(),
                    new AffiseAppIdProvider(_initPropertiesStorage),
                    new AffisePackageAppNameProvider(),
                    new AppVersionProvider(),
                    new AppVersionRawProvider(),
                    new StoreProvider(),
                    new InstalledTimeProvider(),
                    firstOpenTimeProvider,
                    new InstalledHourProvider(),
                    new FirstOpenHourProvider(_firstAppOpenUseCase),
                    new InstallFirstEventProvider(_firstAppOpenUseCase),
                    new InstallBeginTimeProvider(),
                    new InstallFinishTimeProvider(_firstAppOpenUseCase),
                    new ReferrerInstallVersionProvider(),
                    new ReferrerClickTimestampProvider(),
                    new ReferrerClickTimestampServerProvider(),
                    new ReferrerGooglePlayInstantProvider(),
                    createdTimeProvider,
                    new CreatedTimeMilliProvider(),
                    new CreatedTimeHourProvider(),
                    new CustomLongProvider(ProviderType.LAST_TIME_SESSION, 54.0f, () =>
                    {
                        var lastTimeSession = lastSessionTimeProvider.ProvideWithDefault();
                        if (lastTimeSession > 0)
                        {
                            return lastTimeSession;
                        }
                        return firstOpenTimeProvider.ProvideWithDefault() ?? 0L;
                    }),
                    new ConnectionTypeProvider(),
                    new CpuTypeProvider(),
                    new HardwareNameProvider(),
                    new NetworkTypeProvider(),
                    new DeviceManufacturerProvider(),
                    new DeeplinkClickPropertyProvider(_deeplinkClickRepository),
                    // new EmptyStringProvider(ProviderType.DEVICE_ATLAS_ID, 26.0f),
                    new AffiseDeviceIdProvider(_appUuids),
                    new AffiseAltDeviceIdProvider(_appUuids),
                    new AndroidIdProvider(),
                    new GoogleAdvertisingIdProvider(),
                    new GoogleAdvertisingIdMd5Provider(_stringToMd5Converter),
                    new RefTokenProvider(),
                    new InstallReferrerProvider(),
                    new UserAgentProvider(),
                    new IspNameProvider(),
                    new LanguageProvider(),
                    new DeviceNameProvider(),
                    new DeviceTypeProvider(),
                    new OsNameProvider(),
                    new PlatformNameProvider(),
                    new SdkPlatformNameProvider(),
                    new ApiLevelOSProvider(),
                    new AffSDKVersionProvider(),
                    new OSVersionProvider(),
                    new RandomUserIdProvider(_appUuids),
                    new IsProductionPropertyProvider(_initPropertiesStorage),
                    new TimezoneDeviceProvider(),
                    // new EmptyStringProvider(ProviderType.AFFISE_EVENT_TOKEN, 52.0f),
                    // new EmptyStringProvider(ProviderType.AFFISE_EVENT_NAME, 53.0f),
                    lastSessionTimeProvider,
                    new TimeSessionProvider(_sessionManager),
                    new AffiseSessionCountProvider(_sessionManager),
                    new AffiseAppOpenedProvider(_sessionManager),
                    new LifetimeSessionCountProvider(_sessionManager),
                    new DeeplinkProvider(_deeplinkClickRepository),
                    new AffPartParamNamePropertyProvider(_initPropertiesStorage),
                    new AffPartParamNameTokenPropertyProvider(_initPropertiesStorage),
                    new AffAppTokenPropertyProvider(_initPropertiesStorage, _stringToSHA256Converter),
                    // new EmptyStringProvider(ProviderType.LABEL, 62.0f),
                    // new AffSDKSecretIdProvider(_initPropertiesStorage),
                    new PushTokenProvider(_pushTokenUseCase),
                    new PushTokenServiceProvider(_pushTokenUseCase),
                    // new EmptyStringProvider(ProviderType.OS_AND_VERSION, 68.0f),
                    // new EmptyStringProvider(ProviderType.DEVICE, 69.0f),
                    // new EmptyStringProvider(ProviderType.BUILD, 70.0f),
                }
            );
        }
    }
}