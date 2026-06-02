#nullable enable

using System.Collections.Generic;
using System.Linq;
using AffiseAttributionLib;
using AffiseAttributionLib.Modules;
using AffiseAttributionLib.Settings;
using UnityEngine;


namespace AffiseAppDemo
{
    [RequireComponent(typeof(AlertMessage))]
    public class AffiseService : MonoBehaviour
    {
        private AlertMessage? _alertMessage;

        private void Awake()
        {
            _alertMessage = GetComponent<AlertMessage>();
        }

        private void Start()
        {
            InitAffise();
        }

        private void InitAffise()
        {
            var appSettings = new AppSettings();

            // Initialize https://github.com/affise/sdk-unity#manual
            // For manual init delete "Affise Settings.asset" or disable [isActive] flag  
            Affise
                .Settings(
                    affiseAppId: appSettings.AppId,
                    secretKey: appSettings.SecretKey
                )
                .SetConfigValue(AffiseConfig.FB_APP_ID, "1111111111111111")
                .SetDomain(appSettings.Domain)
                .SetProduction(appSettings.ProductionMode) //To enable debug methods set Production to false
                .SetOnInitSuccess(() =>
                {
                    Debug.Log($"<color=#{LOG_OK_COLOR}>Affise: Affise: init success</color>");
                })
                .SetOnInitError((error) => 
                { 
                    Debug.Log($"<color=#{LOG_ERR_COLOR}>Affise: init error {error}</color>");
                })
                .SetDisableModules(new List<AffiseModules>
                {
                    // Exclude modules from start
                    AffiseModules.Advertising,
                    AffiseModules.Persistent,
                    AffiseModules.Subscription,
                })
                .Start(); // Start Affise SDK

            // Debug: network request/response
            Affise.Debug.Network((request, response) =>
            {
                if (appSettings.DebugRequest)
                {
                    Debug.Log($"<color=#{LOG_NET_COLOR}>Affise: {request}</color>");
                }

                if (appSettings.DebugResponse)
                {
                    Debug.Log($"<color=#{LOG_NET_COLOR}>Affise: {response}</color>");
                }
            });

            // Deeplinks https://github.com/affise/sdk-unity#deeplinks
            Affise.RegisterDeeplinkCallback(value =>
            {
                var list = value.Parameters.Select(h => $"{h.Key}=[{string.Join(", ", h.Value)}]").ToList();
                var parameters = string.Join(", ", list);
                Alert("Deeplink", $"{value.Deeplink}\n\n" +
                                      $"scheme={value.Scheme}\n" +
                                      $"host={value.Host}\n" +
                                      $"path={value.Path}\n" +
                                      $"parameters=[{parameters}]"
                );
            });
        }

        private void Alert(string title, string text)
        {
            _alertMessage?.Show(title, text);
        }

        private const string LOG_OK_COLOR = "009688";
        private const string LOG_ERR_COLOR = "ef5350";
        private const string LOG_NET_COLOR = "64b5f6";
    }
}
