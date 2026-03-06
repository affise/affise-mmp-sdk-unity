using AffiseAttributionLib.AffiseParameters.Factory;
using AffiseAttributionLib.Debugger.Network;
using AffiseAttributionLib.Debugger.Validate;
using AffiseAttributionLib.Deeplink;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Init;
using AffiseAttributionLib.Modules;
using AffiseAttributionLib.Referrer;
using AffiseAttributionLib.Usecase;

namespace AffiseAttributionLib
{
    internal interface IAffiseApi
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
    }
}