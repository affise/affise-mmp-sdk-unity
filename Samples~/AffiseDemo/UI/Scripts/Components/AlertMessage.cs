#nullable enable

using UnityEngine;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    [RequireComponent(typeof(UIDocument))]
    public class AlertMessage : MonoBehaviour
    {
        [SerializeField]
        private string viewName = "AlertLayer";

        private VisualElement? _alertLayer;
        private Label? _alertTitle;
        private Label? _alertText;
        private Button? _alertOkButton;
        private VisualElement? _alertBackdrop;
        private UIDocument? _document;

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            BindAlert();
        }

        private void OnDisable()
        {
            if (_alertOkButton != null)
            {
                _alertOkButton.clicked -= Hide;
            }

            _alertBackdrop?.UnregisterCallback<ClickEvent>(OnBackdropClicked);

            _alertLayer = null;
            _alertTitle = null;
            _alertText = null;
            _alertOkButton = null;
            _alertBackdrop = null;
        }

        public void Show(string title, string text)
        {
            if (_alertLayer == null)
            {
                BindAlert();
            }

            if (_alertLayer == null || _alertTitle == null || _alertText == null)
            {
                Debug.Log($"{title} : {text}");
                return;
            }

            _alertTitle.text = title;
            _alertText.text = text;
            _alertLayer.style.display = DisplayStyle.Flex;
            _alertLayer.pickingMode = PickingMode.Position;
        }

        private void BindAlert()
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

            _alertLayer = root.Q<VisualElement>(viewName);
            _alertTitle = _alertLayer?.Q<Label>("AlertTitle");
            _alertText = _alertLayer?.Q<Label>("AlertText");
            _alertOkButton = _alertLayer?.Q<Button>("AlertOkButton");
            _alertBackdrop = _alertLayer?.Q<VisualElement>("AlertBackdrop");

            if (_alertOkButton != null)
            {
                _alertOkButton.clicked -= Hide;
                _alertOkButton.clicked += Hide;
            }

            if (_alertBackdrop != null)
            {
                _alertBackdrop.UnregisterCallback<ClickEvent>(OnBackdropClicked);
                _alertBackdrop.RegisterCallback<ClickEvent>(OnBackdropClicked);
            }

            Hide();
        }

        private void OnBackdropClicked(ClickEvent evt)
        {
            Hide();
            evt.StopPropagation();
        }

        public void Hide()
        {
            if (_alertLayer == null)
            {
                return;
            }

            _alertLayer.style.display = DisplayStyle.None;
            _alertLayer.pickingMode = PickingMode.Ignore;
        }
    }
}
