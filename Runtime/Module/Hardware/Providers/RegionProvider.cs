#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.Module.Hardware.Providers
{
    internal sealed class RegionProvider : StringPropertyProvider
    {
        private readonly RemarketingUseCase _remarketing;

        public RegionProvider(RemarketingUseCase remarketing) => _remarketing = remarketing;

        public override float Order => 38.0f;
        public override ProviderType? Key => ProviderType.REGION;

        public override string? Provide() => _remarketing.GetCountryCode();
    }
}
