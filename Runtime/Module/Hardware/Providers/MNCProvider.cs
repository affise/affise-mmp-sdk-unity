#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;

namespace AffiseAttributionLib.Module.Hardware.Providers
{
    internal sealed class MNCProvider : StringPropertyProvider
    {
        public override float Order => 37.0f;
        public override ProviderType? Key => ProviderType.MNCODE;

        // The C# runtime does not have access to mobile carrier metadata.
        public override string? Provide() => null;
    }
}
