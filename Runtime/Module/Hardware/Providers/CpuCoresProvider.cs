#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.Module.Hardware.Providers
{
    internal sealed class CpuCoresProvider : LongPropertyProvider
    {
        private readonly ProcessInfoUseCase _processInfo;

        public CpuCoresProvider(ProcessInfoUseCase processInfo) => _processInfo = processInfo;

        public override float Order => 42.4f;
        public override ProviderType? Key => ProviderType.CPU_CORES;

        public override long? Provide() => _processInfo.CpuCores;
    }
}
