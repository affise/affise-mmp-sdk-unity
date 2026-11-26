#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.Module.Hardware.Providers
{
    internal sealed class OsAndVersionProvider : StringPropertyProvider
    {
        private readonly RemarketingUseCase _remarketing;

        public OsAndVersionProvider(RemarketingUseCase remarketing) => _remarketing = remarketing;

        public override float Order => 68.0f;
        public override ProviderType? Key => ProviderType.OS_AND_VERSION;

        public override string Provide() => _remarketing.OsAndVersion;
    }
}
