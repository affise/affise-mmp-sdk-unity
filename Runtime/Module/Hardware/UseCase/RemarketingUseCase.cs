#nullable enable
using System;
using System.Globalization;
using AffiseAttributionLib.Extensions;
using UnityEngine;

namespace AffiseAttributionLib.Module.Hardware.UseCase
{
    internal sealed class RemarketingUseCase
    {
        private readonly string _model = SystemInfo.deviceModel;

        public string? Device => _model == SystemInfo.unsupportedIdentifier ? null : _model;
        public string OsAndVersion { get; } = SystemInfo.operatingSystem;
        public string Language => Application.systemLanguage.ToCode();

        public string? GetBuild()
        {
#if UNITY_EDITOR_WIN || (UNITY_STANDALONE_WIN && !UNITY_EDITOR)
            var build = Environment.OSVersion.Version.Build;
            return build > 0 ? $"Build/{build.ToString(CultureInfo.InvariantCulture)}" : null;
#else
            return null;
#endif
        }

        public string? GetCountryCode()
        {
            var culture = CultureInfo.CurrentCulture;
            if (culture.IsNeutralCulture || culture.Name.Length == 0) return null;

            try
            {
                return new RegionInfo(culture.Name).TwoLetterISORegionName;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
