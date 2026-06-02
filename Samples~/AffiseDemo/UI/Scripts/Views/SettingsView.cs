#nullable enable

using UnityEngine;
using UnityEngine.UIElements;
using AffiseAttributionLib;

namespace AffiseAppDemo
{
    [ExecuteAlways]
    [RequireComponent(typeof(UIDocument))]
    public class SettingsView : MonoBehaviour
    {
        [SerializeField]
        private string layerName = "SettingsLayer";

        [SerializeField]
        private string viewName = "SettingsView";

        public bool IsVisible { get; private set; }

        private VisualElement? _view;
        private VisualElement? _layer;
        private readonly AppSettings _appSettings = new();
        private AffiseTextField? _domainField;
        private AffiseTextField? _appIdField;
        private AffiseTextField? _secretKeyField;
        private Toggle? _productionModeToggle;
        private Toggle? _offlineModeToggle;
        private Toggle? _backgroundTrackingToggle;
        private Toggle? _trackingToggle;
        private Toggle? _debugRequestToggle;
        private Toggle? _debugResponseToggle;
        private Label? _versionLabel;
        private UIDocument? _document;

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            RebindView();
        }

        private void OnDisable()
        {
            UnregisterCallbacks();
            _layer = null;
            _view = null;
            _domainField = null;
            _appIdField = null;
            _secretKeyField = null;
            _productionModeToggle = null;
            _offlineModeToggle = null;
            _backgroundTrackingToggle = null;
            _trackingToggle = null;
            _debugRequestToggle = null;
            _debugResponseToggle = null;
            _versionLabel = null;
        }

        private void OnValidate()
        {
            if (isActiveAndEnabled)
            {
                RebindView();
            }
        }

        public void Show()
        {
            SetVisible(true);
        }

        public void Hide()
        {
            SetVisible(false);
        }

        public void SetVisible(bool isVisible)
        {
            if (_view == null)
            {
                BindViewNow();
            }

            IsVisible = isVisible;
            if (IsVisible)
            {
                RefreshSettings();
            }

            ApplyViewState();
        }

        public bool Toggle()
        {
            SetVisible(!IsVisible);
            return IsVisible;
        }

        private void RebindView()
        {
            UiToolkitUtils.RunWhenActiveAndEnabled(this, BindViewNow);
        }

        private void BindViewNow()
        {
            _document ??= GetComponent<UIDocument>();
            if (_document == null)
            {
                return;
            }

            var root = _document.rootVisualElement;
            if (root == null)
            {
                return;
            }

            _layer = root.Q<VisualElement>(layerName);
            _view = root.Q<VisualElement>(viewName);
            BindFields();
            if (IsVisible)
            {
                RefreshSettings();
            }

            ApplyViewState();
        }

        private void BindFields()
        {
            if (_view == null)
            {
                return;
            }

            UnregisterCallbacks();

            var content = _view.Q<ScrollView>("SettingsContent");
            if (content != null)
            {
                UiToolkitUtils.ConfigureScrollView(content, "settings-content-inner");
            }

            _domainField = _view.Q<AffiseTextField>("DomainField");
            _appIdField = _view.Q<AffiseTextField>("AppIdField");
            _secretKeyField = _view.Q<AffiseTextField>("SecretKeyField");
            _productionModeToggle = _view.Q<Toggle>("ProductionModeToggle");
            _offlineModeToggle = _view.Q<Toggle>("OfflineModeToggle");
            _backgroundTrackingToggle = _view.Q<Toggle>("BackgroundTrackingToggle");
            _trackingToggle = _view.Q<Toggle>("TrackingToggle");
            _debugRequestToggle = _view.Q<Toggle>("DebugRequestToggle");
            _debugResponseToggle = _view.Q<Toggle>("DebugResponseToggle");
            _versionLabel = _view.Q<Label>("VersionLabel");

            RegisterCallbacks();
        }

        private void RefreshSettings()
        {
            _domainField?.SetValueWithoutNotify(_appSettings.Domain);
            _appIdField?.SetValueWithoutNotify(_appSettings.AppId);
            _secretKeyField?.SetValueWithoutNotify(_appSettings.SecretKey);

            _productionModeToggle?.SetValueWithoutNotify(_appSettings.ProductionMode);
            _offlineModeToggle?.SetValueWithoutNotify(_appSettings.OfflineMode);
            _backgroundTrackingToggle?.SetValueWithoutNotify(_appSettings.BackgroundTracking);
            _trackingToggle?.SetValueWithoutNotify(_appSettings.Tracking);
            _debugRequestToggle?.SetValueWithoutNotify(_appSettings.DebugRequest);
            _debugResponseToggle?.SetValueWithoutNotify(_appSettings.DebugResponse);
            if (_versionLabel != null)
            {
                _versionLabel.text = $"version {Affise.Debug.Version()}";
            }
        }

        private void RegisterCallbacks()
        {
            _domainField?.RegisterValueChangedCallback(OnDomainChanged);
            _appIdField?.RegisterValueChangedCallback(OnAppIdChanged);
            _secretKeyField?.RegisterValueChangedCallback(OnSecretKeyChanged);
            _productionModeToggle?.RegisterValueChangedCallback(OnProductionModeChanged);
            _offlineModeToggle?.RegisterValueChangedCallback(OnOfflineModeChanged);
            _backgroundTrackingToggle?.RegisterValueChangedCallback(OnBackgroundTrackingChanged);
            _trackingToggle?.RegisterValueChangedCallback(OnTrackingChanged);
            _debugRequestToggle?.RegisterValueChangedCallback(OnDebugRequestChanged);
            _debugResponseToggle?.RegisterValueChangedCallback(OnDebugResponseChanged);
        }

        private void UnregisterCallbacks()
        {
            _domainField?.UnregisterValueChangedCallback(OnDomainChanged);
            _appIdField?.UnregisterValueChangedCallback(OnAppIdChanged);
            _secretKeyField?.UnregisterValueChangedCallback(OnSecretKeyChanged);
            _productionModeToggle?.UnregisterValueChangedCallback(OnProductionModeChanged);
            _offlineModeToggle?.UnregisterValueChangedCallback(OnOfflineModeChanged);
            _backgroundTrackingToggle?.UnregisterValueChangedCallback(OnBackgroundTrackingChanged);
            _trackingToggle?.UnregisterValueChangedCallback(OnTrackingChanged);
            _debugRequestToggle?.UnregisterValueChangedCallback(OnDebugRequestChanged);
            _debugResponseToggle?.UnregisterValueChangedCallback(OnDebugResponseChanged);
        }

        private void OnDomainChanged(ChangeEvent<string> evt) => _appSettings.Domain = evt.newValue;

        private void OnAppIdChanged(ChangeEvent<string> evt) => _appSettings.AppId = evt.newValue;

        private void OnSecretKeyChanged(ChangeEvent<string> evt) => _appSettings.SecretKey = evt.newValue;

        private void OnProductionModeChanged(ChangeEvent<bool> evt) => _appSettings.ProductionMode = evt.newValue;

        private void OnOfflineModeChanged(ChangeEvent<bool> evt)
        {
            _appSettings.OfflineMode = evt.newValue;
            Affise.SetOfflineModeEnabled(evt.newValue);
        }

        private void OnBackgroundTrackingChanged(ChangeEvent<bool> evt)
        {
            _appSettings.BackgroundTracking = evt.newValue;
            Affise.SetBackgroundTrackingEnabled(evt.newValue);
        }

        private void OnTrackingChanged(ChangeEvent<bool> evt)
        {
            _appSettings.Tracking = evt.newValue;
            Affise.SetTrackingEnabled(evt.newValue);
        }

        private void OnDebugRequestChanged(ChangeEvent<bool> evt) => _appSettings.DebugRequest = evt.newValue;

        private void OnDebugResponseChanged(ChangeEvent<bool> evt) => _appSettings.DebugResponse = evt.newValue;

        private void ApplyViewState()
        {
            if (_layer != null)
            {
                _layer.style.display = IsVisible ? DisplayStyle.Flex : DisplayStyle.None;
                _layer.pickingMode = IsVisible ? PickingMode.Position : PickingMode.Ignore;
            }

            if (_view == null)
            {
                return;
            }

            _view.pickingMode = IsVisible ? PickingMode.Position : PickingMode.Ignore;
        }
    }
}
