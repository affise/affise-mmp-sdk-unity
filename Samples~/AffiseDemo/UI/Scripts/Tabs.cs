#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    [ExecuteAlways]
    [RequireComponent(typeof(UIDocument))]
    public class Tabs : MonoBehaviour
    {
        private const string TabTemplatePath = "Assets/Samples/AffiseDemo/UI/UXML/Tab.uxml";

        public int SelectedTabIndex => _selectedTabIndex;
        public string SelectedTabName => GetTabName(_selectedTabIndex);

        [SerializeField]
        private List<TabData> tabIcons = new List<TabData>
        {
            new() { Label = "API", Icon = null },
            new() { Label = "Events", Icon = null },
            new() { Label = "Store", Icon = null },
        };

        [SerializeField]
        private VisualTreeAsset? tabTemplate;

        [SerializeField]
        private float minTabWidthForText = 120;

        [SerializeField]
        [Range(1, 10)]
        private int defaultTab = 1;

        [SerializeField]
        private UnityEvent<int, string> tabChanged = new();

        private int _selectedTabIndex;
        private VisualElement? _currentTabs;
        private EventCallback<GeometryChangedEvent>? _geometryChangedCallback;
        private UIDocument? _document;

        [Serializable]
        private struct TabData
        {
            public string? Label;
            public Sprite? Icon;
        }

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            EnsureGeometryChangedCallback();
            RebuildTabs();
        }

        private void OnDisable()
        {
            if (_geometryChangedCallback != null)
            {
                _currentTabs?.UnregisterCallback(_geometryChangedCallback);
            }

            _currentTabs = null;
        }

        private void OnValidate()
        {
            defaultTab = Mathf.Clamp(defaultTab, 1, 10);

            if (isActiveAndEnabled)
            {
                RebuildTabs();
            }
        }

        private void RebuildTabs()
        {
            UiToolkitUtils.RunWhenActiveAndEnabled(this, FillTabs);
        }

        private void FillTabs()
        {
            EnsureGeometryChangedCallback();

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

            var tabsElement = root.Q<VisualElement>("Tabs");

            if (tabsElement == null)
            {
                Debug.LogWarning("Tabs element is not found.");
                return;
            }

            if (_geometryChangedCallback != null)
            {
                _currentTabs?.UnregisterCallback(_geometryChangedCallback);
            }

            _currentTabs = tabsElement;

            tabsElement.Clear();

            if (_geometryChangedCallback != null)
            {
                tabsElement.RegisterCallback(_geometryChangedCallback);
            }

            _selectedTabIndex = GetValidTabIndex(defaultTab - 1);

            for (var index = 0; index < tabIcons.Count; index++)
            {
                var tabIcon = tabIcons[index];
                var tab = CreateTab();

                if (tab == null)
                {
                    continue;
                }

                var labelText = tabIcon.Label ?? string.Empty;

                tab.name = labelText;
                tab.EnableInClassList("active", index == _selectedTabIndex);

                var tabIndex = index;
                tab.clicked += () => SelectTab(tabIndex);

                var icon = tab.Q<VisualElement>("TabIcon");
                if (icon != null)
                {
                    icon.pickingMode = PickingMode.Ignore;
                    icon.style.backgroundImage = StyleKeyword.Null;

                    if (tabIcon.Icon != null)
                    {
                        icon.style.backgroundImage = new StyleBackground(Background.FromSprite(tabIcon.Icon));
                    }
                }

                var label = tab.Q<Label>("TabText");
                if (label != null)
                {
                    label.pickingMode = PickingMode.Ignore;
                    label.text = labelText;
                }

                tabsElement.Add(tab);
            }

            tabsElement.schedule.Execute(() => UpdateTabTextVisibility(tabsElement));
            NotifyTabChanged();
        }

        private Button? CreateTab()
        {
            var template = GetTabTemplate();
            if (template != null)
            {
                return template.CloneTree().Q<Button>("Tab");
            }

            var tab = new Button
            {
                name = "Tab",
            };
            tab.AddToClassList("tab");

            var icon = new VisualElement
            {
                name = "TabIcon",
            };
            icon.AddToClassList("tab-icon");

            var label = new Label
            {
                name = "TabText",
            };
            label.AddToClassList("tab-text");

            tab.Add(icon);
            tab.Add(label);

            return tab;
        }

        private VisualTreeAsset? GetTabTemplate()
        {
            if (tabTemplate != null)
            {
                return tabTemplate;
            }

#if UNITY_EDITOR
            tabTemplate = UnityEditor.AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(TabTemplatePath);
#endif
            return tabTemplate;
        }

        private void SelectTab(int index)
        {
            if (_currentTabs == null)
            {
                return;
            }

            _selectedTabIndex = GetValidTabIndex(index);

            var tabs = _currentTabs.Children().OfType<Button>().ToList();
            for (var i = 0; i < tabs.Count; i++)
            {
                tabs[i].EnableInClassList("active", i == _selectedTabIndex);
            }

            NotifyTabChanged();
        }

        private void NotifyTabChanged()
        {
            if (_selectedTabIndex >= 0)
            {
                var tabName = GetTabName(_selectedTabIndex);
                tabChanged.Invoke(_selectedTabIndex, tabName);
            }
        }

        private string GetTabName(int index)
        {
            if (index < 0 || index >= tabIcons.Count)
            {
                return string.Empty;
            }

            return tabIcons[index].Label ?? string.Empty;
        }

        private int GetValidTabIndex(int index)
        {
            if (tabIcons.Count == 0)
            {
                return -1;
            }

            return Mathf.Clamp(index, 0, tabIcons.Count - 1);
        }

        private void UpdateTabTextVisibility(VisualElement? tabsElement)
        {
            if (tabsElement == null)
            {
                return;
            }

            foreach (var tab in tabsElement.Children().OfType<Button>())
            {
                var hideText = tab.resolvedStyle.width < minTabWidthForText;
                var icon = tab.Q<VisualElement>("TabIcon");
                var label = tab.Q<Label>("TabText");

                icon?.EnableInClassList("tab-icon-only", hideText);
                label?.EnableInClassList("tab-text-hidden", hideText);
            }
        }

        private void EnsureGeometryChangedCallback()
        {
            if (_geometryChangedCallback == null)
            {
                _geometryChangedCallback = _ => UpdateTabTextVisibility(_currentTabs);
            }
        }
    }
}
