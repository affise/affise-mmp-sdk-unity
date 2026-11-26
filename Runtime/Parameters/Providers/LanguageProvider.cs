using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.AffiseParameters.Providers
{
    /**
     * Provider for parameter [ProviderType.LANGUAGE]
     */
    internal class LanguageProvider : StringPropertyProvider
    {
        private readonly RemarketingUseCase _remarketing;

        public LanguageProvider(RemarketingUseCase remarketing) => _remarketing = remarketing;

        public override float Order => 40.0f;
        public override ProviderType? Key => ProviderType.LANGUAGE;

        public override string Provide() => _remarketing.Language;
    }
}
