using System;
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters.Logs;
using AffiseAttributionLib.Exceptions;
using SimpleJSON;

namespace AffiseAttributionLib.Logs
{
    internal class LogsManagerImpl : ILogsManager
    {
        private readonly IStoreLogsUseCase _storeLogsUseCase;

        public LogsManagerImpl(IStoreLogsUseCase storeLogsUseCase)
        {
            _storeLogsUseCase = storeLogsUseCase;
        }

        public void AddNetworkError(Exception exception)
        {
            List<JSONObject> errors;
            if (exception.GetType() == typeof(CloudException))
            {
                errors = new List<JSONObject>
                {
                    CreateCloudExceptionJson((CloudException)exception)
                };
            }
            else
            {
                errors = new List<JSONObject>
                {
                    new()
                    {
                        ["network_error"] = exception.StackTrace
                    }
                };
            }

            foreach (var error in errors)
            {
                StoreLog(
                    new AffiseLog.NetworkLog(error)
                );
            }
        }

        public void AddDeviceError(Exception exception)
        {
            StoreLog(
                new AffiseLog.DevicedataLog(
                    value: exception.StackTrace
                )
            );
        }

        public void AddUserError(Exception exception)
        {
            StoreLog(
                new AffiseLog.UserdataLog(
                    value: exception.StackTrace
                )
            );
        }

        public void AddSdkError(Exception exception)
        {
            StoreLog(
                new AffiseLog.SdkLog(
                    value: exception.StackTrace
                )
            );
        }

        public void AddDeviceError(string message)
        {
            StoreLog(
                new AffiseLog.DevicedataLog(message)
            );
        }

        private void StoreLog(AffiseLog logEvent)
        {
            _storeLogsUseCase.StoreLog(logEvent);
        }

        private JSONObject CreateCloudExceptionJson(CloudException cloudException)
        {
            var data = cloudException.Exception.Message;
            long? code = null;

            if (cloudException.Exception is NetworkException networkException)
            {
                code = networkException.Code;
                data = $"AffiseError.network(status={networkException.Code}, message={networkException.Message})";
            }

            return new JSONObject
            {
                //Add url
                ["endpoint"] = cloudException.URL,

                //Add code
                ["code"] = code,

                //Add attempts count
                ["attempts"] = cloudException.Attempts,

                //Add is retry sending
                ["retry"] = cloudException.Retry,

                //Add message
                ["message"] = data
            };
        }
    }
}