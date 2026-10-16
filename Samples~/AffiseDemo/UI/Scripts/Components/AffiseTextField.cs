#nullable enable

using UnityEngine.UIElements;

namespace AffiseAppDemo
{
#if UNITY_6000_0_OR_NEWER
    [UxmlElement]
    public partial class AffiseTextField : VisualElement
#else
    public class AffiseTextField : VisualElement
#endif
    {
#if !UNITY_6000_0_OR_NEWER
        public new class UxmlFactory : UxmlFactory<AffiseTextField, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlBoolAttributeDescription _inputEnabled = new()
            {
                name = "input-enabled",
                defaultValue = false,
            };
            private readonly UxmlStringAttributeDescription _label = new()
            {
                name = "label",
                defaultValue = string.Empty,
            };
            private readonly UxmlBoolAttributeDescription _multiline = new()
            {
                name = "multiline",
                defaultValue = false,
            };
            private readonly UxmlBoolAttributeDescription _clearButton = new()
            {
                name = "clear-button",
                defaultValue = true,
            };

            public override void Init(VisualElement element, IUxmlAttributes attributes, CreationContext context)
            {
                base.Init(element, attributes, context);

                var output = (AffiseTextField)element;
                output.InputEnabled = _inputEnabled.GetValueFromBag(attributes, context);
                output.Label = _label.GetValueFromBag(attributes, context);
                output.Multiline = _multiline.GetValueFromBag(attributes, context);
                output.ShowClearButton = _clearButton.GetValueFromBag(attributes, context);
            }
        }
#endif

        private readonly Label _labelElement;
        private readonly TextField _textField;
        private readonly Button _clearButton;

        public AffiseTextField()
        {
            AddToClassList("affise-text-field");

            _labelElement = new Label();
            _labelElement.AddToClassList("affise-text-field-label");

            var control = new VisualElement();
            control.AddToClassList("affise-text-field-control");

            _textField = new TextField
            {
                name = "Output",
                multiline = false,
                isReadOnly = true,
            };
            _textField.AddToClassList("affise-text-field-input");

            _clearButton = new Button(ClearValue)
            {
                text = string.Empty,
            };
            _clearButton.AddToClassList("affise-text-field-clear-button");
            _clearButton.Add(new VisualElement
            {
                pickingMode = PickingMode.Ignore,
            });
            _clearButton[0].AddToClassList("affise-text-field-clear-icon");

            control.Add(_textField);
            control.Add(_clearButton);
            Add(_labelElement);
            Add(control);

            Label = string.Empty;
        }

#if UNITY_6000_0_OR_NEWER
        [UxmlAttribute("input-enabled")]
#endif
        public bool InputEnabled
        {
            get => !_textField.isReadOnly;
            set => _textField.isReadOnly = !value;
        }

        public string Value
        {
            get => _textField.value;
            set => _textField.value = value ?? string.Empty;
        }

#if UNITY_6000_0_OR_NEWER
        [UxmlAttribute("label")]
#endif
        public string Label
        {
            get => _labelElement.text;
            set
            {
                _labelElement.text = value ?? string.Empty;
                _labelElement.style.display = string.IsNullOrWhiteSpace(_labelElement.text)
                    ? DisplayStyle.None
                    : DisplayStyle.Flex;
            }
        }

#if UNITY_6000_0_OR_NEWER
        [UxmlAttribute("multiline")]
#endif
        public bool Multiline
        {
            get => _textField.multiline;
            set
            {
                _textField.multiline = value;
                EnableInClassList("affise-text-field-multiline", value);
            }
        }

#if UNITY_6000_0_OR_NEWER
        [UxmlAttribute("clear-button")]
#endif
        public bool ShowClearButton
        {
            get => _clearButton.resolvedStyle.display != DisplayStyle.None;
            set => _clearButton.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public void SetValueWithoutNotify(string value)
        {
            _textField.SetValueWithoutNotify(value ?? string.Empty);
        }

        public void RegisterValueChangedCallback(EventCallback<ChangeEvent<string>> callback)
        {
            _textField.RegisterValueChangedCallback(callback);
        }

        public void UnregisterValueChangedCallback(EventCallback<ChangeEvent<string>> callback)
        {
            _textField.UnregisterValueChangedCallback(callback);
        }

        public void Append(string value)
        {
            Value = string.IsNullOrWhiteSpace(Value)
                ? value
                : $"{Value}\n{value}";
        }

        public void ClearValue()
        {
            Value = string.Empty;
        }
    }
}
