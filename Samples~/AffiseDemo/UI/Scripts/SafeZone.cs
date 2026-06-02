#nullable enable

using UnityEngine;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    [ExecuteAlways]
    [RequireComponent(typeof(UIDocument))]
    public class SafeZone : MonoBehaviour
    {
        [SerializeField]
        private string safeZoneName = "SafeZone";

        private VisualElement? _safeZone;
        private Rect _lastSafeArea;
        private Vector2Int _lastScreenSize;
        private Vector2 _lastRootSize;
        private EventCallback<GeometryChangedEvent>? _geometryChangedCallback;
        private UIDocument? _document;

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            EnsureGeometryChangedCallback();
            BindSafeZone();
        }

        private void OnDisable()
        {
            if (_geometryChangedCallback != null)
            {
                _safeZone?.UnregisterCallback(_geometryChangedCallback);
            }

            _safeZone = null;
        }

        private void Update()
        {
            if (HasSafeAreaChanged())
            {
                ApplySafeArea();
            }
        }

        private void OnValidate()
        {
            if (isActiveAndEnabled)
            {
                BindSafeZone();
            }
        }

        private void BindSafeZone()
        {
            UiToolkitUtils.RunWhenActiveAndEnabled(this, BindSafeZoneNow);
        }

        private void BindSafeZoneNow()
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

            if (_geometryChangedCallback != null)
            {
                _safeZone?.UnregisterCallback(_geometryChangedCallback);
            }

            _safeZone = root.Q<VisualElement>(safeZoneName);
            if (_safeZone == null)
            {
                Debug.LogWarning($"{safeZoneName} element is not found.");
                return;
            }

            if (_geometryChangedCallback != null)
            {
                _safeZone.RegisterCallback(_geometryChangedCallback);
            }

            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            if (_safeZone == null)
            {
                BindSafeZoneNow();
            }

            if (_safeZone == null)
            {
                return;
            }

            var root = _safeZone.panel?.visualTree;
            if (root == null || Screen.width <= 0 || Screen.height <= 0)
            {
                return;
            }

            var rootWidth = root.resolvedStyle.width;
            var rootHeight = root.resolvedStyle.height;
            if (rootWidth <= 0 || rootHeight <= 0)
            {
                return;
            }

            var safeArea = Screen.safeArea;
            var left = safeArea.xMin / Screen.width * rootWidth;
            var right = (Screen.width - safeArea.xMax) / Screen.width * rootWidth;
            var bottom = safeArea.yMin / Screen.height * rootHeight;
            var top = (Screen.height - safeArea.yMax) / Screen.height * rootHeight;

            _safeZone.style.paddingLeft = left;
            _safeZone.style.paddingRight = right;
            _safeZone.style.paddingTop = top;
            _safeZone.style.paddingBottom = bottom;

            _lastSafeArea = safeArea;
            _lastScreenSize = new Vector2Int(Screen.width, Screen.height);
            _lastRootSize = new Vector2(rootWidth, rootHeight);
        }

        private bool HasSafeAreaChanged()
        {
            if (_safeZone == null)
            {
                return true;
            }

            var root = _safeZone.panel?.visualTree;
            var rootSize = root != null
                ? new Vector2(root.resolvedStyle.width, root.resolvedStyle.height)
                : Vector2.zero;

            return _lastSafeArea != Screen.safeArea
                || _lastScreenSize.x != Screen.width
                || _lastScreenSize.y != Screen.height
                || _lastRootSize != rootSize;
        }

        private void EnsureGeometryChangedCallback()
        {
            if (_geometryChangedCallback == null)
            {
                _geometryChangedCallback = _ => ApplySafeArea();
            }
        }
    }
}
