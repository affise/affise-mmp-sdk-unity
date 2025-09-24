#nullable enable

using AffiseAttributionLib.Modules;

namespace AffiseAttributionLib.Module.Advertising
{
    internal class AffiseAdvertising: AffiseModuleApiWrapper<IAffiseAdvertisingApi>, IAffiseModuleAdvertisingApi
    {
        protected override AffiseModules Module => AffiseModules.Advertising;
        public void StartModule()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.AdvertisingStartModule();
#else
            ModuleApi?.StartModule();
#endif
        }
        
        public bool HasModule() => IsModuleInit;
    }
}