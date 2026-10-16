#nullable enable
using System.Collections.Generic;
using System.Linq;
using AffiseAttributionLib.Editor.Extensions;
using AffiseAttributionLib.Editor.Ui;
using UnityEngine.UIElements;

namespace AffiseAttributionLib.Editor.Elements
{
#if UNITY_6000_0_OR_NEWER
    [UxmlElement]
    internal partial class ModuleList : VisualElement, INotifyValueChanged<IEnumerable<Modules.Module>>
#else
    internal class ModuleList : VisualElement, INotifyValueChanged<IEnumerable<Modules.Module>>
#endif
    {
        #region UXML

#if !UNITY_6000_0_OR_NEWER
        public new class UxmlFactory : UxmlFactory<ModuleList, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
            }
        }
#endif

        #endregion UXML

        public ModuleList() : this(
            new List<Modules.Module>(),
            new Dictionary<string, string?>()
        )
        {
        }

        public ModuleList(
            IEnumerable<Modules.Module> modules,
            IReadOnlyDictionary<string, string?> comments
        )
        {
            UI.Get(nameof(ModuleList)).ToRoot(this);
            var modulesView = this.Q<VisualElement>("modules");

            value = modules.OrderBy(o => o.name);

            foreach (var module in value)
            {
                if (comments.ContainsKey(module.name))
                { 
                    module.tooltip = comments[module.name];
                }
                var moduleElement = new ModuleElement(module);
                moduleElement.RegisterValueChangedCallback(OnChangeVal);
                modulesView.Add(moduleElement);
            }
        }

        private void OnChangeVal(ChangeEvent<Modules.Module> evt)
        {
            SetNewValue(evt.newValue);
        }

        private void SetNewValue(Modules.Module newModule)
        {
            if (panel is null) return;
            var newValue = new List<Modules.Module>();

            foreach (var module in value)
            {
                if (module.name == newModule.name)
                {
                    newValue.Add(module.Copy(newModule.android, newModule.ios));
                }
                else
                {
                    newValue.Add(module);
                }
            }

            using (var evt = ChangeEvent<IEnumerable<Modules.Module>>.GetPooled(value, newValue))
            {
                evt.target = this;
                SetValueWithoutNotify(newValue);
                SendEvent(evt);
            }
        }

        public void SetValueWithoutNotify(IEnumerable<Modules.Module> newValue)
        {
            value = newValue;
        }

        public IEnumerable<Modules.Module> value { get; set; }
    }
}
