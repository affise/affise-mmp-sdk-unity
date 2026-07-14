#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using AffiseAttributionLib.AffiseParameters.Factory;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Exceptions;
using AffiseAttributionLib.Executors;
using AffiseAttributionLib.Internal;
using AffiseAttributionLib.Logs;
using AffiseAttributionLib.Network;
using AffiseAttributionLib.Network.Entity;
using AffiseAttributionLib.Utils;

namespace AffiseAttributionLib.Usecase
{
    internal class SendDataToServerUseCaseImpl : ISendDataToServerUseCase
    {
        private const long TIME_DELAY_SENDING = 3 * 1000;

        private readonly IExecutorServiceProvider _executorServiceProvider;
        private readonly PostBackModelFactory _postBackModelFactory;
        private readonly ICloudRepository _cloudRepository;
        private readonly IEventsRepository _eventsRepository;
        private readonly IInternalEventsRepository _internalEventsRepository;
        private readonly ILogsRepository _logsRepository;
        private readonly ILogsManager _logsManager;
        private readonly FirstAppOpenUseCase _firstAppOpenUseCase;

        private readonly Dictionary<string, bool> _lockSend = CloudConfig.GetUrls().ToDictionary(
            key => key,
            value => false
        );

        private readonly Dictionary<string, int> _attemptSend = CloudConfig.GetUrls().ToDictionary(
            key => key,
            value => 0
        );

        public SendDataToServerUseCaseImpl(
            IExecutorServiceProvider executorServiceProvider,
            PostBackModelFactory postBackModelFactory,
            ICloudRepository cloudRepository,
            IEventsRepository eventsRepository,
            IInternalEventsRepository internalEventsRepository,
            ILogsRepository logsRepository,
            ILogsManager logsManager,
            FirstAppOpenUseCase firstAppOpenUseCase
        )
        {
            _executorServiceProvider = executorServiceProvider;
            _postBackModelFactory = postBackModelFactory;
            _cloudRepository = cloudRepository;
            _eventsRepository = eventsRepository;
            _internalEventsRepository = internalEventsRepository;
            _logsRepository = logsRepository;
            _logsManager = logsManager;
            _firstAppOpenUseCase = firstAppOpenUseCase;
        }

        public void Send(bool withDelay = true, bool sendEmpty = true)
        {
            foreach (var url in CloudConfig.GetUrls())
            {
                if (_lockSend[url]) continue;

                _lockSend[url] = true;

                if (withDelay)
                {
                    SendWithDelay(url, sendEmpty, () =>
                    {
                        _lockSend[url] = false;
                        _attemptSend[url] = 0;
                    });
                }
                else
                {
                    Send(url, sendEmpty, () =>
                    {
                        _lockSend[url] = false;
                        _attemptSend[url] = 0;
                    });
                }
            }
        }

        private bool IsToSendWithDelay(string url)
        {
            return _eventsRepository.HasEvents(url)
                || _internalEventsRepository.HasEvents(url)
                || _logsRepository.HasLogs(url);
        }

        private void Send(string url, bool sendEmpty, Action onComplete)
        {
            //Get events
            var events = _eventsRepository.GetEvents(url);

            //Get logs
            var logs = _logsRepository.GetLogs(url);

            //Get internal events
            var internalEvents = _internalEventsRepository.GetEvents(url);

            if (!sendEmpty && !(events.Count != 0 || internalEvents.Count != 0 || logs.Count != 0))
            {
                // if flag sendEmpty is false and all array is empty
                // don't send empty postback
                onComplete.Invoke();
                return;
            }

            // Send data for single url
            _cloudRepository.Send(
                PostBackModelsData(events, logs, internalEvents),
                url,
                response =>
                {
                    if (response.IsValid())
                    {
                        DeleteEvent(events, url);
                        DeleteInternalEvent(internalEvents, url);
                        DeleteLog(logs, url);

                        if (IsToSendWithDelay(url))
                        {
                            SendWithDelay(url, sendEmpty, onComplete);
                        }
                        else
                        {
                            if (_firstAppOpenUseCase.IsFirstOpen()) {
                                // Complete first open
                                _firstAppOpenUseCase.CompleteFirstOpen();
                            }
                            onComplete.Invoke();
                        }
                    }
                    else
                    {
                        //Log error
                        _logsManager.AddNetworkError(new CloudException(
                            url: url,
                            exception: new NetworkException(
                                code: response.Code, 
                                message: null
                            ), 
                            attempts: _attemptSend[url], 
                            retry: true
                        ));
                        _attemptSend[url] += 1;
                        SendWithDelay(url, sendEmpty, onComplete);  
                    }
                }
            );
        }

        private void SendWithDelay(string url, bool sendEmpty, Action onComplete)
        {
            _executorServiceProvider.ExecuteWithDelay(TIME_DELAY_SENDING, () =>
            {
                Send(url, sendEmpty, onComplete);
            });
        }

        private List<PostBackModel> PostBackModelsData(
            List<SerializedEvent> events,
            List<SerializedLog> logs,
            List<SerializedEvent> internalEvents
        )
        {
            var data = _postBackModelFactory.Create(events, logs, internalEvents);
            // If first run
            if (_firstAppOpenUseCase.IsFirstOpen())
            {
                data = data.AsFirstOpen();
            }
            
            return new List<PostBackModel>
            {
                data
            };
        }

        private void DeleteEvent(List<SerializedEvent> events, string url)
        {
            var ids = events.Select(s => s.Id);
            // Remove all sent events
            _eventsRepository.DeleteEvent(ids, url);
        }

        private void DeleteInternalEvent(List<SerializedEvent> events, string url)
        {
            var ids = events.Select(s => s.Id);
            // Remove all sent internal events
            _internalEventsRepository.DeleteEvent(ids, url);
        }

        private void DeleteLog(List<SerializedLog> logs, string url)
        {
            var ids = logs.Select(s => s.Id);
            // Remove all sent logs
            _logsRepository.DeleteLogs(ids, url);
        }
    }
}
