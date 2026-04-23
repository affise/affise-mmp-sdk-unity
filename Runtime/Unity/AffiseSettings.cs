#nullable enable
using AffiseAttributionLib.Init;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace AffiseAttributionLib.Unity
{
    internal sealed class AffiseSettings : ScriptableObject
    {
        internal const string ConfigName = "affise.settings";

        #region Variables

        [SerializeField] 
        private string appId = "";

        [SerializeField] 
        private string secretId = "";
        
        [SerializeField] 
        private string? partParamName;

        [SerializeField] 
        private string? partParamNameToken;

        [SerializeField] 
        private string? appToken;

        [SerializeField] 
        private string? domain;
        
        [SerializeField] 
        private bool isProduction = true;

        [SerializeField] 
        public bool isActive;

        #endregion Variables

        #region Init settings

        private static AffiseSettings? _instance;

        private static AffiseSettings? Instance => _instance ??= GetSettingsAsset();

        private static AffiseSettings? GetSettingsAsset()
        {
            LoadFromResources(out var settings);
            return settings;
        }

        private static void LoadFromResources(out AffiseSettings? settings)
        {
            foreach (var asset in Resources.LoadAll<AffiseSettings>(""))
            {
                if (asset.isActive == false) continue;
                settings = asset;
                return;
            }

            settings = null;
        }

        #endregion Init settings

        #region Init Affise

        // Load Affise after level starts
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void OnSceneLoadInitialize()
        {
            if (_instance is not null) return;
            if (Instance is null) return;

            Instance.Init();
        }

        private void Init()
        {
            if (Affise.IsInit) return;

            var props = new AffiseInitProperties(
                affiseAppId: appId,
                secretKey: secretId,
                partParamName: partParamName,
                partParamNameToken: partParamNameToken,
                appToken: appToken,
                isProduction: isProduction,
                domain: domain
            );

            AffiseInternal.Start(props);
        }

        #endregion Init Affise
    }
}