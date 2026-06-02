#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    internal static class UiToolkitUtils
    {
        public static void RunWhenActiveAndEnabled(MonoBehaviour behaviour, Action action)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += () =>
            {
                if (behaviour != null && behaviour.isActiveAndEnabled)
                {
                    action();
                }
            };
#else
            action();
#endif
        }

        public static void ConfigureScrollView(
            ScrollView scrollView,
            string contentClass = "events-list-content",
            string scrollbarClass = "events-list-scrollbar"
        )
        {
            scrollView.contentContainer.AddToClassList(contentClass);
            scrollView.verticalScroller.AddToClassList(scrollbarClass);
        }

        public static List<string> EnumNames<TEnum>()
            where TEnum : struct, Enum
        {
            return Enum.GetNames(typeof(TEnum)).ToList();
        }

        public static List<string> EnumValueNames<TEnum>()
            where TEnum : struct, Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(value => value.ToString())
                .ToList();
        }
    }
}
