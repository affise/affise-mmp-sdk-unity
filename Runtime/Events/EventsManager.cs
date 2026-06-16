#nullable enable
using AffiseAttributionLib.Executors;
using AffiseAttributionLib.Usecase;

namespace AffiseAttributionLib.Events
{
    internal class EventsManager
    {
        private const long TIME_SEND_REPEAT = 15 * 1000;
        private const long TIME_SEND_SECOND = 3 * 1000;
        private const long SCHEDULER_SEND_REPEAT = 15 * 1000;

        private readonly ISendDataToServerUseCase _sendDataToServerUseCase;
        private readonly IExecutorServiceProvider _executorServiceProvider;

        public EventsManager(
            IExecutorServiceProvider executorServiceProvider,
            ISendDataToServerUseCase sendDataToServerUseCase
        )
        {
            _executorServiceProvider = executorServiceProvider;
            _sendDataToServerUseCase = sendDataToServerUseCase;
        }

        public void Init()
        {
            sendEventsOnStart();
        }

        public void SendEvents(bool withDelay = true, bool sendEmpty = true)
        {
            _sendDataToServerUseCase.Send(withDelay, sendEmpty);
        }

        private void sendEventsOnStart()
        {
            //Send events on start
            SendEvents(false);
            
            //Send events on delay
            SendEventsDelay(delay: TIME_SEND_SECOND);
                
            //Start timer fo repeat send events
            StartTimer();
            
            //Start Scheduler
            StartQueue();
        }

        private void SendEventsDelay(long delay)
        {
            _executorServiceProvider.ExecuteWithDelay(delay, () =>
            {
                SendEvents(withDelay: false);
            });
        }

        private void StartTimer()
        {
            _executorServiceProvider.ExecuteWithDelay(TIME_SEND_REPEAT, () =>
            {
                SendEvents();
            });
        }

        private void StartQueue()
        {
            _executorServiceProvider.ExecuteWithDelay(SCHEDULER_SEND_REPEAT, () =>
            {
                SendEvents(false, false);
                StartQueue();
            });
        }
    }
}