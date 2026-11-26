using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.AffiseParameters.Providers
{
    /**
     * Provider for parameter [ProviderType.CPU_TYPE]
     */
    internal class CpuTypeProvider : StringPropertyProvider
    {
        private readonly ProcessInfoUseCase _processInfo;

        public CpuTypeProvider(ProcessInfoUseCase processInfo) => _processInfo = processInfo;

        public override float Order => 22.0f;
        public override ProviderType? Key => ProviderType.CPU_TYPE;

        public override string Provide() => _processInfo.CpuType;
    }
}
