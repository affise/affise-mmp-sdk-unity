#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Events.Custom;
using AffiseAttributionLib.Module.AppsFlyer;
using AffiseAttributionLib.Modules;


namespace AffiseAttributionLib.Module.Link
{
    public class AppsFlyerModule : AffiseModule, IAffiseAppsFlyerApi
    {
        private const string CATEGORY = "appsflyer";
        public override void Start()
        {
        }
        
        public void LogEvent<T>(string eventName, Dictionary<string, T> eventValues)
        {
            new UserCustomEvent(eventName: eventName, category: CATEGORY)
                .InternalAddRawParameters(eventValues)
                .Send();
        }
    }
}