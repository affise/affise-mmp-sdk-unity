#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events
{
    public enum AutoCatchingType
    {
        BUTTON,
        TEXT,

        IMAGE_BUTTON,
        IMAGE,

        GROUP
    }
    
    internal static class AutoCatchingTypeExtensions
    {
        public static string ToValue(this AutoCatchingType type)
        {
            return type switch
            {
                AutoCatchingType.BUTTON => "BUTTON",
                AutoCatchingType.TEXT => "TEXT",
                
                AutoCatchingType.IMAGE_BUTTON => "IMAGE_BUTTON",
                AutoCatchingType.IMAGE => "IMAGE",
                
                AutoCatchingType.GROUP => "GROUP",
                _ => ""
            };
        }

        public static JSONArray ToJsonArray(this List<AutoCatchingType> types)
        {
            var result = new JSONArray();
            foreach (var type in types)
            {
                result.Add(type.ToValue());
            }
            return result;
        }
    }
}