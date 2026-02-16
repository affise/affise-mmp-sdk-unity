#nullable enable
using System;
using System.Collections.Generic;
using AffiseAttributionLib.Debugger.Network;

namespace AffiseAttributionLib.Network
{
    public interface IHttpClient
    {
        public enum Method
        {
            GET,
            POST
        }

        System.Collections.IEnumerator ExecuteRequest(
            string httpsUrl,
            Method method,
            string? data,
            Dictionary<string, string> headers,
            Action<HttpResponse>? onComplete = null,
            bool redirect = true,
            bool skipBody = false
        );

        void SetDebug(DebugOnNetworkCallback debugNetwork);
    }
}