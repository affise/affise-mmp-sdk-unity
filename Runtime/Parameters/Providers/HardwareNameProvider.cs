using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.AffiseParameters.Providers
{
    /**
     * Provider for parameter [ProviderType.HARDWARE_NAME]
     */
    internal class HardwareNameProvider : StringPropertyProvider
    {
        private readonly HardwareInfoUseCase _hardwareInfo;

        public HardwareNameProvider(HardwareInfoUseCase hardwareInfo) => _hardwareInfo = hardwareInfo;

        public override float Order => 23.0f;
        public override ProviderType? Key => ProviderType.HARDWARE_NAME;

        public override string Provide() => _hardwareInfo.HardwareName;
    }
}
