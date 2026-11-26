#nullable enable
using AffiseAttributionLib.Utils;
using UnityEngine;

namespace AffiseAttributionLib.Module.Hardware.UseCase
{
    internal sealed class ProcessInfoUseCase
    {
        private readonly int _count = SystemInfo.processorCount;

        public string CpuType => Cpu.Type();
        public long? CpuCores => _count > 0 ? _count : null;
    }
}
