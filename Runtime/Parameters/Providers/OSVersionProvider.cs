using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.AffiseParameters.Providers
{
    /**
     * Provider for parameter [ProviderType.OS_VERSION]
     */
    internal class OSVersionProvider : StringPropertyProvider
    {
        private readonly HardwareInfoUseCase _hardwareInfo;

        public OSVersionProvider(HardwareInfoUseCase hardwareInfo) => _hardwareInfo = hardwareInfo;

        public override float Order => 48.0f;
        public override ProviderType? Key => ProviderType.OS_VERSION;

        public override string Provide() => _hardwareInfo.OSVersion;
    }
}
