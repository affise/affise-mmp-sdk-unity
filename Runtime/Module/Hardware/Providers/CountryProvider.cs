#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.Module.Hardware.Providers
{
    internal sealed class CountryProvider : StringPropertyProvider
    {
        private readonly RemarketingUseCase _remarketing;

        public CountryProvider(RemarketingUseCase remarketing) => _remarketing = remarketing;

        public override float Order => 39.0f;
        public override ProviderType? Key => ProviderType.COUNTRY;

        public override string? Provide() => _remarketing.GetCountryCode();
    }
}
