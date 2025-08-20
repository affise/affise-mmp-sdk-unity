#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Modules;

namespace AffiseAttributionLib.Module.TikTok
{
    public interface IAffiseTikTokApi : IAffiseModuleApi
    {
        void SendEvent<T>(string? eventName, Dictionary<string, T>? properties, string? eventId);
    }
}