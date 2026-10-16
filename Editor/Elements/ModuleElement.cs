#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Editor.Extensions;
using AffiseAttributionLib.Editor.Ui;
using UnityEngine.UIElements;

namespace AffiseAttributionLib.Editor.Elements
{
#if UNITY_6000_0_OR_NEWER
    [UxmlElement]
    internal partial class ModuleElement : VisualElement, INotifyValueChanged<Modules.Module>
#else
    internal class ModuleElement : VisualElement, INotifyValueChanged<Modules.Module>
#endif
    {
        #region UXML

#if !UNITY_6000_0_OR_NEWER
        public new class UxmlFactory : UxmlFactory<ModuleElement, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _moduleAttribute = new() { name = "Module" };
            private readonly UxmlBoolAttributeDescription _androidAttribute = new() { name = "Android" };
            private readonly UxmlBoolAttributeDescription _iosAttribute = new() { name = "Ios" };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var cmp = (ModuleElement)ve;

                cmp.Module = _moduleAttribute.GetValueFromBag(bag, cc);

                cmp.value.androidModule = _androidAttribute.GetValueFromBag(bag, cc);
                cmp.value.iosModule = _iosAttribute.GetValueFromBag(bag, cc);

                cmp._label.text = _moduleAttribute.GetValueFromBag(bag, cc);
                cmp._toggleAndroid.Visible(_androidAttribute.GetValueFromBag(bag, cc));
                cmp._toggleIos.Visible(_iosAttribute.GetValueFromBag(bag, cc));
            }
        }
#endif

        #endregion UXML

        private string _module = string.Empty;
        private bool _showAndroid;
        private bool _showIos;

#if UNITY_6000_0_OR_NEWER
        [UxmlAttribute("Module")]
#endif
        public string Module
        {
            get => _module;
            set
            {
                _module = value ?? string.Empty;
                BindData();
            }
        }

#if UNITY_6000_0_OR_NEWER
        [UxmlAttribute("Android")]
#endif
        public bool ShowAndroid
        {
            get => _showAndroid;
            set
            {
                _showAndroid = value;
                BindData();
            }
        }

#if UNITY_6000_0_OR_NEWER
        [UxmlAttribute("Ios")]
#endif
        public bool ShowIos
        {
            get => _showIos;
            set
            {
                _showIos = value;
                BindData();
            }
        }

        public Modules.Module value { get; set; }

        private readonly Label? _label;
        private readonly Toggle? _toggleAndroid;
        private readonly Toggle? _toggleIos;

        public ModuleElement() : this(new Modules.Module())
        {
        }

        public ModuleElement(Modules.Module module)
        {
            Module = module.name;
            ShowAndroid = module.androidModule;
            ShowIos = module.iosModule;
            value = module;

            UI.Get(nameof(ModuleElement)).ToRoot(this);
            _label = this.Q<Label>("name");
            _toggleAndroid = this.Q<Toggle>("android");
            _toggleIos = this.Q<Toggle>("ios");

            this.tooltip = module.tooltip;
            _toggleAndroid.value = module.android;
            _toggleIos.value = module.ios;

            _toggleAndroid.RegisterValueChangedCallback(OnChangePlatform);
            _toggleIos.RegisterValueChangedCallback(OnChangePlatform);

            BindData();
        }

        private void BindData()
        {
            if (_label is null || _toggleAndroid is null || _toggleIos is null) return;

            _label.text = Module;
            _toggleAndroid.Visible(ShowAndroid);
            _toggleIos.Visible(ShowIos);
        }

        private void OnChangePlatform(ChangeEvent<bool> evt)
        {
            if (evt.previousValue == evt.newValue) return;

            if (_toggleAndroid is null || _toggleIos is null) return;

            var newValue = value.Copy(_toggleAndroid.value, _toggleIos.value);

            SetNewValue(newValue);
        }

        private void SetNewValue(Modules.Module newValue)
        {
            if (panel is null) return;
            using (var evt = ChangeEvent<Modules.Module>.GetPooled(value, newValue))
            {
                evt.target = this;
                SetValueWithoutNotify(newValue);
                SendEvent(evt);
            }
        }

        public void SetValueWithoutNotify(Modules.Module newValue)
        {
            value = newValue;
        }
    }
}
