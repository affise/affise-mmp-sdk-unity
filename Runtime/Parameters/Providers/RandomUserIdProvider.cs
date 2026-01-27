using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Usecase;

namespace AffiseAttributionLib.AffiseParameters.Providers
{
    /**
     * Provider for parameter [ProviderType.RANDOM_USER_ID]
     */
    internal class RandomUserIdProvider : StringPropertyProvider
    {
        public override float Order => 49.0f;
        public override ProviderType? Key => ProviderType.RANDOM_USER_ID;
        
        private readonly IAppUUIDs _useCase;

        public RandomUserIdProvider(IAppUUIDs appUuids)
        {
            _useCase = appUuids;
        }

        public override string Provide()
        {
            return _useCase.GetRandomUserId();
        }
    }
}