#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;
using AffiseAttributionLib.Events.Parameters;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    public sealed class NewPredefined : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<NewPredefined, UxmlTraits>
        {
        }

        public event Action<PredefinedData>? OnPredefinedAdded;

        private readonly DropdownField _predefinedType;
        private readonly DropdownField _predefined;
        private readonly AffiseTextField _predefinedValue;
        private readonly Button _addButton;

        public NewPredefined()
        {
            AddToClassList("new-predefined");

            _predefinedType = new DropdownField(PredefinedTypes(), 0);
            _predefinedType.AddToClassList("predefined-dropdown");
            _predefinedType.RegisterValueChangedCallback(_ =>
            {
                RefreshPredefinedValues();
                RefreshAddButtonState();
            });

            _predefined = new DropdownField(PredefinedValues(PredefinedType.PREDEFINED_FLOAT), 0);
            _predefined.AddToClassList("predefined-dropdown");

            _predefinedValue = new AffiseTextField
            {
                Label = string.Empty,
                InputEnabled = true,
                Multiline = false,
                ShowClearButton = true,
            };
            _predefinedValue.RegisterValueChangedCallback(_ => RefreshAddButtonState());

            _addButton = new Button(AddPredefined)
            {
                text = "Add Predefined",
            };
            _addButton.AddToClassList("event-button");
            _addButton.AddToClassList("predefined-add-button");

            Add(_predefinedType);
            Add(_predefined);
            Add(_predefinedValue);
            Add(_addButton);
            RefreshAddButtonState();
        }

        public void Bind(PredefinedData data)
        {
            _predefinedType.SetValueWithoutNotify(data.predefinedType.ToString());
            RefreshPredefinedValues();

            if (_predefined.choices.Contains(data.predefined))
            {
                _predefined.SetValueWithoutNotify(data.predefined);
            }

            _predefinedValue.SetValueWithoutNotify(FormatValue(data.data));
            RefreshAddButtonState();
        }

        private void RefreshPredefinedValues()
        {
            _predefined.choices = PredefinedValues(CurrentType());
            _predefined.index = _predefined.choices.Count > 0 ? 0 : -1;
        }

        private void AddPredefined()
        {
            if (!TryCreateValue(CurrentType(), _predefinedValue.Value, out var value))
            {
                return;
            }

            var type = CurrentType();

            OnPredefinedAdded?.Invoke(new PredefinedData(type, _predefined.value, value));
            _predefinedValue.ClearValue();
            RefreshAddButtonState();
        }

        private PredefinedType CurrentType()
        {
            return Enum.TryParse(_predefinedType.value, out PredefinedType type)
                ? type
                : PredefinedType.PREDEFINED_FLOAT;
        }

        private void RefreshAddButtonState()
        {
            _addButton.SetEnabled(TryCreateValue(CurrentType(), _predefinedValue.Value, out _));
        }

        private static bool TryCreateValue(PredefinedType type, string value, out object data)
        {
            data = string.Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            switch (type)
            {
                case PredefinedType.PREDEFINED_FLOAT when TryParseFloat(value, out var floatValue):
                    data = floatValue;
                    return true;
                case PredefinedType.PREDEFINED_LONG when long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var longValue):
                    data = longValue;
                    return true;
                case PredefinedType.PREDEFINED_STRING:
                    data = value;
                    return true;
                default:
                    return false;
            }
        }

        private static bool TryParseFloat(string value, out float result)
        {
            return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result)
                || float.TryParse(value.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out result);
        }

        private static string FormatValue(object? value)
        {
            return value switch
            {
                float floatValue => floatValue.ToString(CultureInfo.InvariantCulture),
                double doubleValue => doubleValue.ToString(CultureInfo.InvariantCulture),
                long longValue => longValue.ToString(CultureInfo.InvariantCulture),
                int intValue => intValue.ToString(CultureInfo.InvariantCulture),
                _ => value?.ToString() ?? string.Empty,
            };
        }

        private static List<string> PredefinedTypes()
        {
            return UiToolkitUtils.EnumNames<PredefinedType>();
        }

        private static List<string> PredefinedValues(PredefinedType type)
        {
            return type switch
            {
                PredefinedType.PREDEFINED_FLOAT => UiToolkitUtils.EnumNames<PredefinedFloat>(),
                PredefinedType.PREDEFINED_LONG => UiToolkitUtils.EnumNames<PredefinedLong>(),
                PredefinedType.PREDEFINED_STRING => UiToolkitUtils.EnumNames<PredefinedString>(),
                _ => new List<string>(),
            };
        }
    }
}
