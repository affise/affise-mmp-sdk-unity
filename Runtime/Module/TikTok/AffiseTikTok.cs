#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Modules;

namespace AffiseAttributionLib.Module.TikTok
{
    internal class AffiseTikTok: AffiseModuleApiWrapper<IAffiseTikTokApi>, IAffiseModuleTikTokApi
    {
        protected override AffiseModules Module => AffiseModules.TikTok;
        public void SendEvent<T>(string? eventName, Dictionary<string, T>? properties, string? eventId)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.TikTokSendEvent(eventName, properties, eventId);
#else
            ModuleApi?.SendEvent(eventName, properties, eventId);
#endif
        }
        
        public bool HasModule() => IsModuleInit;
    }
}