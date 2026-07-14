using AffiseAttributionLib.Executors;
using AffiseAttributionLib.Network;

namespace AffiseAttributionLib.Internal
{
    internal class StoreInternalEventUseCaseImpl : IStoreInternalEventUseCase
    {
        private readonly IExecutorServiceProvider _executorServiceProvider;
        private readonly IInternalEventsRepository _repository;

        public StoreInternalEventUseCaseImpl(
            IExecutorServiceProvider executorServiceProvider,
            IInternalEventsRepository repository
        )
        {
            _executorServiceProvider = executorServiceProvider;
            _repository = repository;
        }

        public void StoreInternalEvent(InternalEvent internalEvent)
        {
            _executorServiceProvider.ExecuteWithDelay(0, () =>
            {
                _repository.StoreEvent(internalEvent, CloudConfig.GetUrls());
            });
        }
    }
}
