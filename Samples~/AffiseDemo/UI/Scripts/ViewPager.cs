#nullable enable

using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    [ExecuteAlways]
    [RequireComponent(typeof(UIDocument))]
    public class ViewPager : MonoBehaviour
    {
        [SerializeField]
        private string viewPagerName = "ViewPager";

        private VisualElement? _viewPager;
        private UIDocument? _document;

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            RebuildViewPager();
        }

        private void OnDisable()
        {
            _viewPager = null;
        }

        private void OnValidate()
        {
            if (isActiveAndEnabled)
            {
                RebuildViewPager();
            }
        }

        private void RebuildViewPager()
        {
            UiToolkitUtils.RunWhenActiveAndEnabled(this, BindViewPager);
        }

        private void BindViewPager()
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

            _viewPager = root.Q<VisualElement>(viewPagerName);
            if (_viewPager == null)
            {
                Debug.LogWarning($"{viewPagerName} element is not found.");
                return;
            }

            ShowTab(0, string.Empty);
        }

        public void ShowTab(int selectedIndex, string tabName)
        {
            if (_viewPager == null)
            {
                BindViewPager();
            }

            if (_viewPager == null)
            {
                return;
            }

            var pages = _viewPager.Children().ToList();
            for (var index = 0; index < pages.Count; index++)
            {
                pages[index].EnableInClassList("active", index == selectedIndex);
            }
        }
    }
}
