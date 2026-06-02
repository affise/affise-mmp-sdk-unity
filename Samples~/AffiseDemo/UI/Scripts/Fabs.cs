#nullable enable

using UnityEngine;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    [ExecuteAlways]
    [RequireComponent(typeof(UIDocument))]
    public class Fabs : MonoBehaviour
    {
        private const string EventsTabName = "Events";

        [SerializeField]
        private string viewName = "FabLayer";

        private Button? _eventsFab;
        private Button? _settingsFab;
        private VisualElement? _settingsFabIcon;
        private VisualElement? _predefinedFabIcon;
        private SettingsView? _settingsView;
        private PredefinedView? _predefinedView;
        private Tabs? _tabs;
        private UIDocument? _document;
        private readonly AppSettings _appSettings = new();
        private string _currentTabName = string.Empty;

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
            _settingsView = GetComponent<SettingsView>();
            _predefinedView = GetComponent<PredefinedView>();
            _tabs = GetComponent<Tabs>();
        }

        private void OnEnable()
        {
            AppSettings.UseCustomPredefinedChanged -= OnUseCustomPredefinedChanged;
            AppSettings.UseCustomPredefinedChanged += OnUseCustomPredefinedChanged;
            BindFabs();
            UpdateFabs();
        }

        private void OnDisable()
        {
            if (_settingsFab != null)
            {
                _settingsFab.clicked -= ToggleSettingsView;
            }

            if (_eventsFab != null)
            {
                _eventsFab.clicked -= TogglePredefinedView;
            }

            AppSettings.UseCustomPredefinedChanged -= OnUseCustomPredefinedChanged;

            _eventsFab = null;
            _settingsFab = null;
            _settingsFabIcon = null;
            _predefinedFabIcon = null;
            _currentTabName = string.Empty;
        }

        private void OnValidate()
        {
            if (isActiveAndEnabled)
            {
                BindFabs();
            }
        }

        public void OnTabChanged(int index, string tabName)
        {
            _currentTabName = tabName;
            UpdateFabs();
        }

        private void BindFabs()
        {
            UiToolkitUtils.RunWhenActiveAndEnabled(this, BindFabsNow);
        }

        private void BindFabsNow()
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

            var fabLayer = root.Q<VisualElement>(viewName);
            if (fabLayer != null)
            {
                fabLayer.pickingMode = PickingMode.Ignore;
                if (fabLayer.parent != null)
                {
                    fabLayer.parent.pickingMode = PickingMode.Ignore;
                }
            }

            _eventsFab = fabLayer?.Q<Button>("EventsFab");
            if (_eventsFab != null)
            {
                _eventsFab.pickingMode = PickingMode.Position;
                _eventsFab.clicked -= TogglePredefinedView;
                _eventsFab.clicked += TogglePredefinedView;
            }

            _settingsFab = fabLayer?.Q<Button>("SettingsFab");
            if (_settingsFab != null)
            {
                _settingsFab.pickingMode = PickingMode.Position;
                _settingsFab.clicked -= ToggleSettingsView;
                _settingsFab.clicked += ToggleSettingsView;
            }

            _settingsFabIcon = fabLayer?.Q<VisualElement>("SettingsFabIcon");
            _predefinedFabIcon = fabLayer?.Q<VisualElement>("EventsFabIcon");
            _settingsView ??= GetComponent<SettingsView>();
            _predefinedView ??= GetComponent<PredefinedView>();
            _tabs ??= GetComponent<Tabs>();

            _currentTabName = _tabs != null ? _tabs.SelectedTabName : string.Empty;
            UpdateFabs();
        }

        private void UpdateFabs()
        {
            var isSettingsVisible = _settingsView != null && _settingsView.IsVisible;
            var isPredefinedVisible = _predefinedView != null && _predefinedView.IsVisible;
            var isEventsTab = _currentTabName == EventsTabName;
            var isEventsFabVisible = isEventsTab && !isSettingsVisible;
            var isSettingsFabVisible = !isPredefinedVisible;

            SetFabVisible(_eventsFab, isEventsFabVisible);
            SetFabVisible(_settingsFab, isSettingsFabVisible);
            UpdateEventsFab(isPredefinedVisible);
            UpdateSettingsFab(isSettingsVisible);
        }

        private void ToggleSettingsView()
        {
            if (_settingsView == null)
            {
                return;
            }

            if (_settingsView.Toggle() && _predefinedView != null)
            {
                _predefinedView.Hide();
            }

            UpdateFabs();
        }

        private void TogglePredefinedView()
        {
            if (_predefinedView == null)
            {
                return;
            }

            if (_predefinedView.Toggle() && _settingsView != null)
            {
                _settingsView.Hide();
            }

            UpdateFabs();
        }

        private void UpdateSettingsFab(bool isSettingsVisible)
        {
            _settingsFabIcon?.EnableInClassList("fab-icon-check", isSettingsVisible);
        }

        private void UpdateEventsFab(bool isPredefinedVisible)
        {
            _predefinedFabIcon?.EnableInClassList("fab-icon-check", isPredefinedVisible);
            _eventsFab?.EnableInClassList("events-fab-custom-predefined", _appSettings.UseCustomPredefined);
        }

        private void OnUseCustomPredefinedChanged(bool isEnabled)
        {
            _eventsFab?.EnableInClassList("events-fab-custom-predefined", isEnabled);
        }

        private static void SetFabVisible(Button? button, bool isVisible)
        {
            if (button == null)
            {
                return;
            }

            button.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
            button.pickingMode = isVisible ? PickingMode.Position : PickingMode.Ignore;
        }
    }
}
