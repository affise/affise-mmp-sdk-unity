#nullable enable

using System;
using System.Collections.Generic;
using AffiseAttributionLib.Events.Parameters;
using System.Linq;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Events.Subscription;
using UnityEngine;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    [ExecuteAlways]
    [RequireComponent(typeof(UIDocument))]
    public class EventsView : MonoBehaviour
    {
        [SerializeField]
        private string eventsViewName = "EventsView";

        private readonly AppSettings _appSettings = new();
        private readonly List<(Button Button, Action Callback)> _eventButtons = new();

        private IEventsFactory _eventsFactory = new DefaultEventsFactory();
        private UIDocument? _document;
        private VisualElement? _view;

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            AppSettings.UseCustomPredefinedChanged -= OnUseCustomPredefinedChanged;
            AppSettings.UseCustomPredefinedChanged += OnUseCustomPredefinedChanged;
            RecreateEventsFactory();
            RebuildView();
        }

        private void OnDisable()
        {
            AppSettings.UseCustomPredefinedChanged -= OnUseCustomPredefinedChanged;
            ClearButtons();
            _view = null;
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

            _view = root.Q<VisualElement>(eventsViewName);
            if (_view == null)
            {
                Debug.LogWarning($"{eventsViewName} element is not found.");
                return;
            }

            ClearButtons();
            _view.Clear();

            var scrollView = new ScrollView(ScrollViewMode.Vertical);
            scrollView.AddToClassList("events-list");
            UiToolkitUtils.ConfigureScrollView(scrollView);
            _view.Add(scrollView);

            foreach (var affiseEvent in _eventsFactory.CreateEvents())
            {
                AddEventButton(scrollView, affiseEvent);
            }
        }

        private void AddEventButton(VisualElement parent, AffiseEvent affiseEvent)
        {
            var button = new Button
            {
                text = ToCamelCase(affiseEvent.GetName()),
            };

            button.AddToClassList("event-button");
            if (affiseEvent is BaseSubscriptionEvent)
            {
                button.AddToClassList("subscription-event-button");
            }

            Action callback = () =>
            {
                ApplyCustomPredefined(affiseEvent);
                affiseEvent.Send();
                Debug.Log($"Send {affiseEvent.GetName()} event.");
            };

            button.clicked += callback;
            _eventButtons.Add((button, callback));
            parent.Add(button);
        }

        private void RecreateEventsFactory()
        {
            _eventsFactory = _appSettings.UseCustomPredefined
                ? new SimpleEventsFactory()
                : new DefaultEventsFactory();
        }

        private void ApplyCustomPredefined(AffiseEvent affiseEvent)
        {
            if (!_appSettings.UseCustomPredefined)
            {
                return;
            }

            foreach (var data in _appSettings.PredefinedDataList)
            {
                ApplyCustomPredefined(affiseEvent, data);
            }
        }

        private void OnUseCustomPredefinedChanged(bool isEnabled)
        {
            RecreateEventsFactory();
            RebuildView();
        }

        private static void ApplyCustomPredefined(AffiseEvent affiseEvent, PredefinedData data)
        {
            switch (data.predefinedType)
            {
                case PredefinedType.PREDEFINED_FLOAT:
                    if (Enum.TryParse(data.predefined, out PredefinedFloat predefinedFloat) && data.data is float floatValue)
                    {
                        affiseEvent.AddPredefinedParameter(predefinedFloat, floatValue);
                    }

                    break;
                case PredefinedType.PREDEFINED_LONG:
                    if (Enum.TryParse(data.predefined, out PredefinedLong predefinedLong) && data.data is long longValue)
                    {
                        affiseEvent.AddPredefinedParameter(predefinedLong, longValue);
                    }

                    break;
                case PredefinedType.PREDEFINED_STRING:
                    if (Enum.TryParse(data.predefined, out PredefinedString predefinedString))
                    {
                        affiseEvent.AddPredefinedParameter(predefinedString, data.data?.ToString() ?? string.Empty);
                    }

                    break;
            }
        }

        private void ClearButtons()
        {
            foreach (var (button, callback) in _eventButtons)
            {
                button.clicked -= callback;
            }

            _eventButtons.Clear();
        }

        private static string ToCamelCase(string input)
        {
            var words = input.Split('_');
            var list = new List<string>();
            foreach (var word in words)
            {
                if (string.IsNullOrEmpty(word))
                {
                    continue;
                }

                list.Add(word.First().ToString().ToUpper() + word.Substring(1));
            }

            return string.Join(string.Empty, list);
        }
    }
}
