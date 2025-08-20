#nullable enable
using System;
using System.Collections.Generic;
using AffiseAttributionLib.Extensions;
using SimpleJSON;

namespace AffiseAttributionLib.Utils
{
    public static class Utils
    {
        public static Dictionary<string, object?>? JsonToDictionary(string? json)
        {
            if (json is null) return null;
            try
            {
                return JSONNode.Parse(json).ToDictionary();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}