#nullable enable
using System;
using System.Linq;
using AffiseAttributionLib.Executors;
using AffiseAttributionLib.Network;
using AffiseAttributionLib.Utils;


namespace AffiseAttributionLib.Module.Link.UseCase
{
    internal class LinkResolveUseCaseImpl : ILinkResolveUseCase
    {
        private const long MAX_REDIRECT_COUNT = 10;
        private const string HEADER_LOCATION = "location";

        private readonly IHttpClient _httpClient;
        private readonly IExecutorServiceProvider _executorServiceProvider;

        public LinkResolveUseCaseImpl(IHttpClient httpClient, IExecutorServiceProvider executorServiceProvider)
        {
            _httpClient = httpClient;
            _executorServiceProvider = executorServiceProvider;
        }

        public void LinkResolve(string url, AffiseLinkCallback callback)
        {
            Resolve(url, MAX_REDIRECT_COUNT, callback);
        }

        private void Resolve(string url, long maxRedirectCount, AffiseLinkCallback callback)
        {
            //Create request
            CreateRequest(url, (response) =>
            {
                //Has redirect status
                if (response.IsRedirect() && maxRedirectCount > 0)
                {
                    var redirectUrl = response.Headers
                        .FirstOrDefault(kvp => kvp.Key.ToLower() == HEADER_LOCATION).Value?
                        .FirstOrDefault(f => !string.IsNullOrWhiteSpace(f));

                    if (redirectUrl is not null)
                    {
                        Resolve(redirectUrl, maxRedirectCount - 1, callback);
                    }
                    else
                    {
                        //Return final url
                        callback.Invoke(url);
                    }
                }
                else
                {
                    //Return final url
                    callback.Invoke(url);
                }
            });
        }

        private void CreateRequest(string url, Action<HttpResponse> onComplete)
        {
            _executorServiceProvider.Execute(
                _httpClient.ExecuteRequest(
                    httpsUrl: url,
                    method: IHttpClient.Method.GET,
                    data: null,
                    headers: CloudConfig.Headers,
                    onComplete: onComplete,
                    redirect: false,
                    skipBody: true
                )
            );
        }
    }
}