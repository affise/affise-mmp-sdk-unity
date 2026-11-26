#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;

namespace AffiseAttributionLib.Module.Hardware.Providers
{
    internal sealed class MCCProvider : StringPropertyProvider
    {
        public override float Order => 36.0f;
        public override ProviderType? Key => ProviderType.MCCODE;

        // The C# runtime does not have access to mobile carrier metadata.
        public override string? Provide() => null;
    }
}
