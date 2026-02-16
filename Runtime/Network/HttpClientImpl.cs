#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AffiseAttributionLib.Debugger.Network;
using UnityEngine.Networking;

namespace AffiseAttributionLib.Network
{
    public class HttpClientImpl : IHttpClient
    {
        private DebugOnNetworkCallback? _debugRequest;

        public void SetDebug(DebugOnNetworkCallback debugNetwork)
        {
            _debugRequest = debugNetwork;
        }

        public IEnumerator ExecuteRequest(
            string httpsUrl,
            IHttpClient.Method method,
            string? data,
            Dictionary<string, string> headers,
            Action<HttpResponse>? onComplete = null,
            bool redirect = true,
            bool skipBody = false
        )
        {
            //Create connection
            var request = new UnityWebRequest(httpsUrl);

            if (data is not null)
            {
                //Create data bytes
                var encodedData = Encoding.UTF8.GetBytes(data);
                request.uploadHandler = new UploadHandlerRaw(encodedData);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.disposeDownloadHandlerOnDispose = true;

            if (skipBody)
            {
                // request only first byte
                request.SetRequestHeader("Range", "bytes=0-0"); 
                request.SetRequestHeader("Accept-Encoding", "identity");
            }

            request.method = RequestMethod(method);
            if (redirect == false)
            {
                request.redirectLimit = 0;
            }

            //Add headers
            foreach (var (key, value) in headers)
            {
                request.SetRequestHeader(key, value);
            }
            
            if (!skipBody)
            {
                //Send request
                yield return request.SendWebRequest();
            }
            else
            {
                //Send request
                request.SendWebRequest();
                
                while (!request.isDone)
                {
                    // If skipping download body and progress is greater then 0
                    if (request.downloadProgress > 0.0f)
                    {
                        // Abort if something is downloading
                        request.Abort();
                        break;
                    }
            
                    // Wait until the next frame before checking again
                    yield return null;
                }
            }

            var responseCode = request.responseCode;
            var responseMessage = request.error ?? "";
            var responseBody = request.downloadHandler.text;
            var responseHeaders = new Dictionary<string, List<string>>();
            
            foreach (var (key, value) in request.GetResponseHeaders())
            {
                if (key is null) continue;
                responseHeaders[key] = new List<string>((value ?? "").Split(';'));;
            }

            var response = new HttpResponse(
                code: responseCode,
                message: responseMessage,
                body: responseBody,
                headers: responseHeaders
            );
            
            // Complete request
            onComplete?.Invoke(response);
            
            // Debug network
            _debugRequest?.Invoke(
                request: new HttpRequest(
                    url: httpsUrl, 
                    method: method, 
                    headers: headers,
                    body: data
                ), 
                response: response
            );
            
            //Disconnect
            request.Dispose();
        }

        private static string RequestMethod(IHttpClient.Method method)
        {
            return method switch
            {
                IHttpClient.Method.GET => UnityWebRequest.kHttpVerbGET,
                IHttpClient.Method.POST => UnityWebRequest.kHttpVerbPOST,
                _ => UnityWebRequest.kHttpVerbPOST
            };
        }
    }
}