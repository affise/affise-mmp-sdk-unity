#nullable enable
using System.Collections.Generic;
using System.Globalization;
using SimpleJSON;

namespace AffiseAttributionLib.Extensions
{
    internal static class JSONObjectExt
    {
        public static JSONNode? ToJsonNode(this object? value)
        {
            if (value is null) return JSONNull.CreateOrGet();
            var json = value switch
            {
                string stringValue => new JSONString(stringValue),
                long longValue => new JSONNumber(longValue.ToString()),
                int intValue => new JSONNumber(intValue.ToString()),
                float floatValue => new JSONNumber(floatValue.ToString(CultureInfo.InvariantCulture)),
                double doubleValue => new JSONNumber(doubleValue.ToString(CultureInfo.InvariantCulture)),
                bool boolValue => new JSONBool(boolValue),
                JSONNode jsonNodeValue => jsonNodeValue,
                Dictionary<string, string> mapValue => mapValue.ToJsonObject(),
                Dictionary<string, object> mapValue => mapValue.ToJsonObject(),
                List<JSONObject> listValue => listValue.ToJsonArray(),
                List<Dictionary<string, string>> listValue => listValue.ToJsonArray(),
                List<Dictionary<string, object>> listValue => listValue.ToJsonArray(),
                List<string> listValue => listValue.ToJsonArray(),
                List<long> listValue => listValue.ToJsonArray(),
                List<int> listValue => listValue.ToJsonArray(),
                List<float> listValue => listValue.ToJsonArray(),
                List<double> listValue => listValue.ToJsonArray(),
                List<bool> listValue => listValue.ToJsonArray(),
                List<object> listValue => listValue.ToJsonArray(),
                _ => JSONNull.CreateOrGet()
            };
            return json;
        }

        public static JSONObject AddAny(this JSONObject json, string key, object? value)
        {
            json[key] = value?.ToJsonNode();
            return json;
        }

        public static JSONArray AddAny(this JSONArray json, object? value)
        {
            json.Add(value?.ToJsonNode());
            return json;
        }

        public static JSONArray ToJsonArray<T>(this List<T> list)
        {
            var result = new JSONArray();
            foreach (var item in list)
            {
                if (item is null) continue;
                result.Add(item.ToJsonNode());
            }

            return result;
        }

        public static JSONObject ToJsonObject<T>(this Dictionary<string, T> map)
        {
            var result = new JSONObject();
            foreach (var (key, value) in map)
            {
                if (key is null) continue;
                if (value is null) continue;
                result[key] = value.ToJsonNode();
            }

            return result;
        }

        public static Dictionary<string, string> ToMapOfStrings(this JSONObject json)
        {
            var result = new Dictionary<string, string>();
            
            foreach (var (key, value) in json)
            {
                if (key is null) continue;
                result.Add(key, value.Value);
            }

            return result;
        }
        
        public static Dictionary<string, List<string>> ToMapOfStringList(this JSONObject json)
        {
            var result = new Dictionary<string, List<string>>();
            
            foreach (var (key, value) in json)
            {
                if (key is null) continue;
                result.Add(key, value.AsArray?.ToListOfStrings() ?? new List<string>());
            }

            return result;
        }
        
        public static List<string> ToListOfStrings(this JSONArray data)
        {
            var result = new List<string>();
            foreach (var (key, value) in data)
            {
                result.Add(value.Value);
            }

            return result;
        }

        public static List<object> ToList(this JSONNode json)
        {
            var result = new List<object>();
            if (!json.IsArray) return result;
            foreach (var (key, value) in json)
            {
                if (value.IsObject)
                {
                    result.Add(value.ToDictionary());
                }
                else if (value.IsArray)
                {
                    result.Add(value.ToList());
                } 
                else if (value.IsNull)
                {
                    continue;
                }
                else
                {
                    result.Add(value.Value);
                }
            }

            return result;
        }

        public static Dictionary<string, object?> ToDictionary(this JSONNode json)
        {
            var result = new Dictionary<string, object?>();
            
            if (!json.IsObject) return result;

            foreach (var (key, value) in json)
            {
                if (key is null) continue;

                if (value.IsObject)
                {
                    result.Add(key, value.ToDictionary());
                }
                else if (value.IsArray)
                {
                    result.Add(key, value.ToList());
                }
                else if (value.IsNull) 
                {
                    result.Add(key, null);
                } 
                else 
                {
                    result.Add(key, value.Value);
                }
            }
            
            return result;
        } 
    }
}