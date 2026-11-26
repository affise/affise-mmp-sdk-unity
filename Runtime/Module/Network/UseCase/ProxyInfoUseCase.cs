#nullable enable
using System;
using System.Linq;
using System.Net;
using AffiseAttributionLib.Network;

namespace AffiseAttributionLib.Module.Network.UseCase
{
    internal sealed class ProxyInfoUseCase
    {
        public string? GetProxyHost()
        {
            var url = CloudConfig.GetUrls().FirstOrDefault();
            if (!Uri.TryCreate(url, UriKind.Absolute, out var target)) return null;

            try
            {
                var proxy = WebRequest.GetSystemWebProxy()?.GetProxy(target);
                return proxy is not null && proxy != target ? proxy.Host : null;
            }
            catch (Exception exception) when (exception is NotSupportedException or WebException)
            {
                return null;
            }
        }
    }
}
