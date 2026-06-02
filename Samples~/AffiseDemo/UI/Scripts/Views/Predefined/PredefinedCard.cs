#nullable enable

using System;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    public sealed class PredefinedCard : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<PredefinedCard, UxmlTraits>
        {
        }

        public event Action<PredefinedCard, PredefinedData>? OnDeleteRequested;
        public event Action<PredefinedData>? OnSelected;

        private readonly Label _predefined;
        private readonly Label _value;

        public PredefinedData? Data { get; private set; }

        public PredefinedCard()
        {
            AddToClassList("predefined-card");

            var content = new VisualElement();
            content.AddToClassList("predefined-card-content");

            _predefined = new Label();
            _predefined.AddToClassList("predefined-card-name");

            _value = new Label();
            _value.AddToClassList("predefined-card-value");

            content.Add(_predefined);
            content.Add(_value);

            var deleteButton = new Button(OnDeleteClicked)
            {
                text = string.Empty,
            };
            deleteButton.RegisterCallback<ClickEvent>(evt => evt.StopPropagation());
            deleteButton.AddToClassList("predefined-card-delete-button");
            deleteButton.Add(new VisualElement
            {
                pickingMode = PickingMode.Ignore,
            });
            deleteButton[0].AddToClassList("predefined-card-delete-icon");

            Add(content);
            Add(deleteButton);
            RegisterCallback<ClickEvent>(OnCardClicked);
        }

        public void Bind(PredefinedData data)
        {
            Data = data;
            _predefined.text = $"{data.predefinedType}.{data.predefined}";
            _value.text = data.data?.ToString() ?? string.Empty;
        }

        private void OnDeleteClicked()
        {
            if (Data != null)
            {
                OnDeleteRequested?.Invoke(this, Data);
            }
        }

        private void OnCardClicked(ClickEvent evt)
        {
            if (Data != null)
            {
                OnSelected?.Invoke(Data);
            }
        }
    }
}
