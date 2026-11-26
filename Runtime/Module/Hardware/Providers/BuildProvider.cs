#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.Module.Hardware.Providers
{
    internal sealed class BuildProvider : StringPropertyProvider
    {
        private readonly RemarketingUseCase _remarketing;

        public BuildProvider(RemarketingUseCase remarketing) => _remarketing = remarketing;

        public override float Order => 70.0f;
        public override ProviderType? Key => ProviderType.BUILD;

        public override string? Provide() => _remarketing.GetBuild();
    }
}
