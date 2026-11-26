#nullable enable
using AffiseAttributionLib.Extensions;
using AffiseAttributionLib.Utils;
using UnityEngine.Device;

namespace AffiseAttributionLib.Module.Hardware.UseCase
{
    internal sealed class HardwareInfoUseCase
    {
        public string DeviceName => SystemInfo.deviceName;
        public string HardwareName => SystemInfo.deviceModel;
        public string DeviceType => SystemInfo.deviceType.ToCustom();
        public string OSVersion => OSUtils.GetOSVersion();
    }
}
