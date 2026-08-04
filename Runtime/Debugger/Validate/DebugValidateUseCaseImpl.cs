#nullable enable
using System;
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.AffiseParameters.Factory;
using AffiseAttributionLib.Converter;
using AffiseAttributionLib.Executors;
using AffiseAttributionLib.Init;
using AffiseAttributionLib.Logs;
using AffiseAttributionLib.Network;
using AffiseAttributionLib.SimpleJSON;
using UnityEngine;

namespace AffiseAttributionLib.Debugger.Validate
{
    internal class DebugValidateUseCaseImpl : IDebugValidateUseCase
    {
        private const string Path = "postback/validate";
        private readonly string _url = CloudConfig.GetUrl(Path);

        private const string KEY = "message";
        private const string INVALID_APP_ID = "Invalid affise_app_id";
        private const string INVALID_CHECK_SUM = "Failed to get application or check sum";
        private const string PACKAGE_NAME_NOT_FOUND = "Package name not found";

        private const int ATTEMPTS_TO_SEND = 2;
        private const long TIME_DELAY_SENDING = 5 * 1000;

        private readonly AffiseInitProperties _initProperties;
        private readonly ILogsManager _logsManager;
        private readonly IHttpClient _httpClient;
        private readonly IExecutorServiceProvider _executorServiceProvider;
        private readonly ProvidersToJsonStringConverter _converter;

        private readonly List<Provider> _providers;
        private int _attempts = ATTEMPTS_TO_SEND;

        public DebugValidateUseCaseImpl(AffiseInitProperties initProperties,
            PostBackModelFactory postBackModelFactory,
            ILogsManager logsManager,
            IHttpClient httpClient,
            IExecutorServiceProvider executorServiceProvider,
            ProvidersToJsonStringConverter converter)
        {
            _initProperties = initProperties;
            _logsManager = logsManager;
            _httpClient = httpClient;
            _executorServiceProvider = executorServiceProvider;
            _converter = converter;
            _providers = postBackModelFactory.GetProviders().GetRequestProviders();
        }

        public void Validate(DebugOnValidateCallback onComplete)
        {
            if (_initProperties.IsProduction)
            {
                onComplete(ValidationStatus.NOT_WORKING_ON_PRODUCTION);
                return;
            }

            SendWithRepeat(onComplete);
        }

        private void SendWithRepeat(DebugOnValidateCallback onComplete)
        {
            CreateRequest((response) =>
            {
                var status = GetValidationStatus(response);
                if (status is not null)
                {
                    onComplete((ValidationStatus)status);
                }
                else
                {
                    if (--_attempts == 0)
                    {
                        onComplete(ValidationStatus.NETWORK_ERROR);
                    }
                    else
                    {
                        SendWithDelay(onComplete);
                    }
                }
            });
        }

        private void SendWithDelay(DebugOnValidateCallback onComplete)
        {
            _executorServiceProvider.ExecuteWithDelay(TIME_DELAY_SENDING, () => { SendWithRepeat(onComplete); });
        }

        private void CreateRequest(
            Action<HttpResponse>? onComplete = null
        )
        {
            _executorServiceProvider.Execute(
                _httpClient.ExecuteRequest(
                    _url,
                    IHttpClient.Method.POST,
                    _converter.Convert(_providers),
                    CloudConfig.Headers,
                    onComplete
                )
            );
        }

        private ValidationStatus? GetValidationStatus(HttpResponse response)
        {
            if (response.Code == 0)
            {
                return ValidationStatus.NETWORK_ERROR;
            }

            try
            {
                var json = JSON.Parse(response.Body ?? "{}").AsObject;
                var message = json[KEY]?.Value ?? "";

                if (message.Equals(INVALID_APP_ID, StringComparison.InvariantCultureIgnoreCase))
                {
                    return ValidationStatus.INVALID_APP_ID;
                }
                if (message.Equals(INVALID_CHECK_SUM, StringComparison.InvariantCultureIgnoreCase))
                {
                    return ValidationStatus.INVALID_SECRET_KEY;
                }
                if (message.Equals(PACKAGE_NAME_NOT_FOUND, StringComparison.InvariantCultureIgnoreCase))
                {
                    return ValidationStatus.PACKAGE_NAME_NOT_FOUND;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            if (response.Code == 200)
            {
                return ValidationStatus.VALID;
            }
            
            return null;
        }
    }
}