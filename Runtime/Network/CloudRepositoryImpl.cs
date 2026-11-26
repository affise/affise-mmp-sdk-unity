#nullable enable
using System;
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters.Providers;
using AffiseAttributionLib.Converter;
using AffiseAttributionLib.Executors;
using AffiseAttributionLib.Network.Entity;

namespace AffiseAttributionLib.Network
{
    public class CloudRepositoryImpl : ICloudRepository
    {
        private readonly IHttpClient _httpClient;
        private readonly UserAgentProvider? _userAgentProvider;
        private readonly IConverter<List<PostBackModel>, string> _postBackModelToJsonStringConverter;
        private readonly IExecutorServiceProvider _executorServiceProvider;

        public CloudRepositoryImpl(
            IExecutorServiceProvider executorServiceProvider,
            IHttpClient httpClient,
            UserAgentProvider? userAgentProvider,
            IConverter<List<PostBackModel>, string> postBackModelToJsonStringConverter)
        {
            _executorServiceProvider = executorServiceProvider;
            _httpClient = httpClient;
            _userAgentProvider = userAgentProvider;
            _postBackModelToJsonStringConverter = postBackModelToJsonStringConverter;
        }

        public void Send(
            List<PostBackModel> data,
            string url,
            Action<HttpResponse>? onComplete = null
        )
        {
            CreateRequest(url, data, onComplete);
        }

        private void CreateRequest(
            string url,
            List<PostBackModel> data,
            Action<HttpResponse>? onComplete = null
        )
        {
            _executorServiceProvider.Execute(
                _httpClient.ExecuteRequest(
                    url,
                    IHttpClient.Method.POST,
                    _postBackModelToJsonStringConverter.Convert(data),
                    CreateHeaders(),
                    onComplete
                )
            );
        }

        private Dictionary<string, string> CreateHeaders()
        {
            return new Dictionary<string, string>
            {
                { "Content-Type", "application/json; charset=utf-8" }
            };
        }
    }
}