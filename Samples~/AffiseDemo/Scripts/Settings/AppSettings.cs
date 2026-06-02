#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace AffiseAppDemo
{
    public sealed class AppSettings
    {
        private const string Prefix = "AffiseDemo.";

        private const string DefaultDomain = "https://tracking.affattr.com";
        private const string DefaultAppId = "129";
        private const string DefaultSecretKey = "93a40b54-6f12-443f-a250-ebf67c5ee4d2";

        public static event Action<bool>? UseCustomPredefinedChanged;

        public string Domain
        {
            get => GetString(nameof(Domain), DefaultDomain);
            set => SetString(nameof(Domain), value);
        }

        public bool ProductionMode
        {
            get => GetBool(nameof(ProductionMode), false);
            set => SetBool(nameof(ProductionMode), value);
        }

        public bool OfflineMode
        {
            get => GetBool(nameof(OfflineMode), false);
            set => SetBool(nameof(OfflineMode), value);
        }

        public bool BackgroundTracking
        {
            get => GetBool(nameof(BackgroundTracking), true);
            set => SetBool(nameof(BackgroundTracking), value);
        }

        public bool Tracking
        {
            get => GetBool(nameof(Tracking), true);
            set => SetBool(nameof(Tracking), value);
        }

        public bool DebugRequest
        {
            get => GetBool(nameof(DebugRequest), false);
            set => SetBool(nameof(DebugRequest), value);
        }

        public bool DebugResponse
        {
            get => GetBool(nameof(DebugResponse), false);
            set => SetBool(nameof(DebugResponse), value);
        }

        public string AppId
        {
            get => GetString(nameof(AppId), DefaultAppId);
            set => SetString(nameof(AppId), value);
        }

        public string SecretKey
        {
            get => GetString(nameof(SecretKey), DefaultSecretKey);
            set => SetString(nameof(SecretKey), value);
        }

        public bool UseCustomPredefined
        {
            get => GetBool(nameof(UseCustomPredefined), false);
            set
            {
                var oldValue = UseCustomPredefined;
                SetBool(nameof(UseCustomPredefined), value);
                if (oldValue != value)
                {
                    UseCustomPredefinedChanged?.Invoke(value);
                }
            }
        }

        public List<PredefinedData> PredefinedDataList
        {
            get => GetPredefinedDataList();
            set => SetPredefinedDataList(value);
        }

        private static string GetString(string key, string defaultValue)
        {
            return PlayerPrefs.GetString(Prefix + key, defaultValue);
        }

        private static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(Prefix + key, value ?? string.Empty);
            PlayerPrefs.Save();
        }

        private static bool GetBool(string key, bool defaultValue)
        {
            return PlayerPrefs.GetInt(Prefix + key, defaultValue ? 1 : 0) == 1;
        }

        private static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(Prefix + key, value ? 1 : 0);
            PlayerPrefs.Save();
        }

        private static List<PredefinedData> GetPredefinedDataList()
        {
            var json = GetString(nameof(PredefinedDataList), string.Empty);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<PredefinedData>();
            }

            var state = JsonUtility.FromJson<PredefinedDataState>(json);
            var result = new List<PredefinedData>();

            if (state?.items == null)
            {
                return result;
            }

            foreach (var item in state.items)
            {
                result.Add(new PredefinedData(
                    item.predefinedType,
                    item.predefined,
                    ParsePredefinedValue(item.predefinedType, item.data)
                ));
            }

            return result;
        }

        private static void SetPredefinedDataList(List<PredefinedData>? value)
        {
            var state = new PredefinedDataState();
            if (value != null)
            {
                foreach (var item in value)
                {
                    state.items.Add(new PredefinedDataItem
                    {
                        predefinedType = item.predefinedType,
                        predefined = item.predefined,
                        data = FormatPredefinedValue(item.data),
                    });
                }
            }

            SetString(nameof(PredefinedDataList), JsonUtility.ToJson(state));
        }

        private static object ParsePredefinedValue(PredefinedType type, string value)
        {
            return type switch
            {
                PredefinedType.PREDEFINED_FLOAT when float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var floatValue) => floatValue,
                PredefinedType.PREDEFINED_LONG when long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var longValue) => longValue,
                _ => value,
            };
        }

        private static string FormatPredefinedValue(object? value)
        {
            return value switch
            {
                float floatValue => floatValue.ToString(CultureInfo.InvariantCulture),
                double doubleValue => doubleValue.ToString(CultureInfo.InvariantCulture),
                long longValue => longValue.ToString(CultureInfo.InvariantCulture),
                int intValue => intValue.ToString(CultureInfo.InvariantCulture),
                _ => value?.ToString() ?? string.Empty,
            };
        }

        [Serializable]
        private sealed class PredefinedDataState
        {
            public List<PredefinedDataItem> items = new List<PredefinedDataItem>();
        }

        [Serializable]
        private sealed class PredefinedDataItem
        {
            public PredefinedType predefinedType;
            public string predefined = string.Empty;
            public string data = string.Empty;
        }
    }
}
