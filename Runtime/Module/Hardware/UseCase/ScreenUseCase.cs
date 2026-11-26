#nullable enable
using UnityEngine;

namespace AffiseAttributionLib.Module.Hardware.UseCase
{
    internal sealed class ScreenUseCase
    {
        private const float ReferenceDpi = 160f;
        private readonly Resolution _resolution = Screen.currentResolution;
        private readonly float _dpi = Screen.dpi;

        public long? Width => _resolution.width > 0 ? _resolution.width : null;
        public long? Height => _resolution.height > 0 ? _resolution.height : null;
        public float? Density => _dpi > 0 && !float.IsInfinity(_dpi) ? _dpi / ReferenceDpi : null;
    }
}
