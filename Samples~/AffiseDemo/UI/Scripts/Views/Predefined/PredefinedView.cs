#nullable enable

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    [ExecuteAlways]
    [RequireComponent(typeof(UIDocument))]
    public class PredefinedView : MonoBehaviour
    {
        [SerializeField]
        private string layerName = "PredefinedLayer";

        [SerializeField]
        private string viewName = "PredefinedView";

        public bool IsVisible => _isVisible;

        private bool _isVisible;
        private VisualElement? _view;
        private VisualElement? _layer;
        private Toggle? _useCustomPredefinedToggle;
        private NewPredefined? _newPredefined;
        private ScrollView? _predefinedList;
        private readonly List<PredefinedData> _predefinedData = new();
        private readonly AppSettings _appSettings = new();
        private UIDocument? _document;

        public bool UseCustomPredefined => _appSettings.UseCustomPredefined;

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
            UnbindFields();
            _layer = null;
            _view = null;
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

            _isVisible = isVisible;
            ApplyViewState();
        }

        public bool Toggle()
        {
            SetVisible(!_isVisible);
            return _isVisible;
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
            ApplyViewState();
        }

        private void BindFields()
        {
            UnbindFields();

            if (_view == null)
            {
                return;
            }

            _useCustomPredefinedToggle = _view.Q<Toggle>("UseCustomPredefinedToggle");
            _newPredefined = _view.Q<NewPredefined>("NewPredefined");
            _predefinedList = _view.Q<ScrollView>("PredefinedList");

            if (_predefinedList != null)
            {
                UiToolkitUtils.ConfigureScrollView(_predefinedList, "predefined-list-content");
            }

            if (_newPredefined != null)
            {
                _newPredefined.OnPredefinedAdded += AddPredefined;
            }

            if (_useCustomPredefinedToggle != null)
            {
                _useCustomPredefinedToggle.RegisterValueChangedCallback(OnUseCustomPredefinedChanged);
            }

            LoadSettings();
            RefreshCards();
        }

        private void UnbindFields()
        {
            if (_newPredefined != null)
            {
                _newPredefined.OnPredefinedAdded -= AddPredefined;
            }

            if (_useCustomPredefinedToggle != null)
            {
                _useCustomPredefinedToggle.UnregisterValueChangedCallback(OnUseCustomPredefinedChanged);
            }
        }

        private void AddPredefined(PredefinedData data)
        {
            var existing = _predefinedData.FirstOrDefault(item =>
                item.predefinedType == data.predefinedType &&
                item.predefined == data.predefined);

            if (existing == null)
            {
                _predefinedData.Add(data);
            }
            else
            {
                existing.data = data.data;
            }

            SavePredefinedData();
            RefreshCards();
        }

        private void RemovePredefined(PredefinedCard card, PredefinedData data)
        {
            card.OnDeleteRequested -= RemovePredefined;
            card.OnSelected -= SelectPredefined;
            _predefinedData.Remove(data);
            SavePredefinedData();
            RefreshCards();
        }

        private void SelectPredefined(PredefinedData data)
        {
            _newPredefined?.Bind(data);
        }

        private void LoadSettings()
        {
            _predefinedData.Clear();
            _predefinedData.AddRange(_appSettings.PredefinedDataList);
            _useCustomPredefinedToggle?.SetValueWithoutNotify(_appSettings.UseCustomPredefined);
        }

        private void SavePredefinedData()
        {
            _appSettings.PredefinedDataList = _predefinedData;
        }

        private void OnUseCustomPredefinedChanged(ChangeEvent<bool> evt)
        {
            _appSettings.UseCustomPredefined = evt.newValue;
        }

        private void RefreshCards()
        {
            if (_predefinedList == null)
            {
                return;
            }

            _predefinedList.Clear();

            foreach (var data in _predefinedData)
            {
                var card = new PredefinedCard();
                card.Bind(data);
                card.OnDeleteRequested += RemovePredefined;
                card.OnSelected += SelectPredefined;
                _predefinedList.Add(card);
            }
        }

        private void ApplyViewState()
        {
            if (_layer != null)
            {
                _layer.style.display = _isVisible ? DisplayStyle.Flex : DisplayStyle.None;
                _layer.pickingMode = _isVisible ? PickingMode.Position : PickingMode.Ignore;
            }

            if (_view == null)
            {
                return;
            }

            _view.pickingMode = _isVisible ? PickingMode.Position : PickingMode.Ignore;
        }
    }
}
