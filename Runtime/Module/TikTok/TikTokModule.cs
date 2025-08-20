#nullable enable

using System.Collections.Generic;
using AffiseAttributionLib.Events.Custom;
using AffiseAttributionLib.Modules;

namespace AffiseAttributionLib.Module.TikTok
{
    public class TikTokModule : AffiseModule, IAffiseTikTokApi
    {
        private const string CATEGORY = "tiktok";
        public override void Start()
        {
        }
        
        public void SendEvent<T>(string? eventName, Dictionary<string, T>? properties, string? eventId)
        {
            if (eventName is null) return;
            new UserCustomEvent(eventName: eventName, category: CATEGORY, userData: eventId)
                .InternalAddRawParameters(properties)
                .Send();
        }
    }
}