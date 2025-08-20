#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Modules;

namespace AffiseAttributionLib.Module.AppsFlyer
{
    internal class AffiseAppsFlyer: AffiseModuleApiWrapper<IAffiseAppsFlyerApi>, IAffiseModuleAppsFlyerApi
    {
        protected override AffiseModules Module => AffiseModules.AppsFlyer;
        public void LogEvent<T>(string eventName, Dictionary<string, T> eventValues)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.AppsFlyerLogEvent(eventName, eventValues);
#else
            ModuleApi?.LogEvent(eventName, eventValues);
#endif
        }
        
        public bool HasModule() => IsModuleInit;
    }
}