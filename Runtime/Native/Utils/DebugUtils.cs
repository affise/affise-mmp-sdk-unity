#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Debugger.Validate;
using AffiseAttributionLib.Extensions;
using AffiseAttributionLib.Network;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Native.Utils
{
    internal static class DebugUtils
    {
        private const string REQUEST = "request";
        private const string RESPONSE = "response";
        private const string METHOD = "method";
        private const string URL = "url";
        private const string HEADERS = "headers";
        private const string BODY = "body";
        private const string CODE = "code";
        private const string MESSAGE = "message";

        public static HttpRequest ToRequest(JSONNode? node)
        {
            var json = node?.AsObject;
            
            var url = json?[URL]?.Value ?? "";
            var method = json?[METHOD]?.Value.ToHttpClientMethod() ?? IHttpClient.Method.POST;
            var body = json?[BODY]?.ToString();
            var headers = json?[HEADERS]?.AsObject?.ToMapOfStrings() ?? new Dictionary<string, string>();

            return new HttpRequest(url, method, headers, body);
        }

        public static HttpResponse ToResponse(JSONNode? node)
        {
            var json = node?.AsObject;
            
            var code = json?[CODE]?.AsLong ?? 0L;
            var message = json?[MESSAGE]?.Value ?? "";
            var body = json?[BODY]?.ToString();
            var headers = json?[HEADERS]?.AsObject?.ToMapOfStringList() ?? new Dictionary<string, List<string>>();

            return new HttpResponse(code, message, body, headers);
        }

        public static (HttpRequest, HttpResponse) ParseRequestResponse(JSONNode? node)
        {
            var json = node?.AsObject;
            return (
                ToRequest(json?[REQUEST]),
                ToResponse(json?[RESPONSE])
            );
        }

        public static ValidationStatus GetValidationStatus(JSONNode? node)
        {
            return node?.Value.ToValidationStatus() ?? ValidationStatus.UNKNOWN_ERROR;
        }
    }
}