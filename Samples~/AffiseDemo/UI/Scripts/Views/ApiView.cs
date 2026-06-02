#nullable enable

using System;
using System.Collections.Generic;
using AffiseAttributionLib.Referrer;
using UnityEngine;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    [ExecuteAlways]
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(AlertMessage))]
    public class ApiView : MonoBehaviour
    {
        [SerializeField]
        private string apiViewName = "ApiView";

        private readonly ApiFactory _apiFactory = new();

        private readonly List<(Button Button, Action Callback)> _apiButtons = new();

        private UIDocument? _document;
        private VisualElement? _view;
        private AffiseTextField? _output;
        private AlertMessage? _alertMessage;

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
            _alertMessage = GetComponent<AlertMessage>();
        }

        private void OnEnable()
        {
            RebuildView();
        }

        private void OnDisable()
        {
            ClearButtons();

            _view = null;
            _output = null;
        }

        private void OnValidate()
        {
            if (isActiveAndEnabled)
            {
                RebuildView();
            }
        }

        private void RebuildView()
        {
            UiToolkitUtils.RunWhenActiveAndEnabled(this, FillView);
        }

        private void FillView()
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

            _view = root.Q<VisualElement>(apiViewName);
            if (_view == null)
            {
                Debug.LogWarning($"{apiViewName} element is not found.");
                return;
            }

            ClearButtons();
            _view.Clear();

            _output = new AffiseTextField
            {
                InputEnabled = false,
                Multiline = true,
                ShowClearButton = true,
            };
            _view.Add(_output);

            _apiFactory.Output = AppendOutput;
            _apiFactory.Alert = Alert;

            var scrollView = new ScrollView(ScrollViewMode.Vertical);
            scrollView.AddToClassList("events-list");
            UiToolkitUtils.ConfigureScrollView(scrollView);
            _view.Add(scrollView);

            AddReferrerDropdown(scrollView);
            AddApiButtons(scrollView);
        }

        private void AddReferrerDropdown(VisualElement parent)
        {
            var dropdown = new DropdownField(ReferrerValues(), 0)
            {
                value = _apiFactory.ReferrerValue
            };
            dropdown.AddToClassList("api-dropdown");
            dropdown.RegisterValueChangedCallback(evt =>
            {
                _apiFactory.ReferrerValue = evt.newValue;
                RebuildView();
            });

            parent.Add(dropdown);
        }

        private void AddApiButtons(VisualElement parent)
        {
            foreach (var (apiName, apiCall) in _apiFactory.Create())
            {
                var button = new Button
                {
                    text = apiName,
                };

                button.AddToClassList("event-button");
                button.AddToClassList("api-button");
                button.clicked += apiCall;
                _apiButtons.Add((button, apiCall));
                parent.Add(button);
            }
        }

        private void ClearButtons()
        {
            foreach (var (button, callback) in _apiButtons)
            {
                button.clicked -= callback;
            }

            _apiButtons.Clear();
        }

        private void AppendOutput(string value)
        {
            if (_output == null)
            {
                return;
            }

            _output.Append(value);
        }

        private void Alert(string title, string message)
        {
            _alertMessage?.Show(title, message);
        }

        private static List<string> ReferrerValues()
        {
            return UiToolkitUtils.EnumValueNames<ReferrerKey>();
        }
    }
}
