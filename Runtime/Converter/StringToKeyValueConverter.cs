#nullable enable
using System;
using System.Collections.Generic;
using AffiseAttributionLib.Modules;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Converter
{
    public class StringToKeyValueConverter : IConverter<string?, List<AffiseKeyValue>>
    {
        private const string KEY = "key";
        private const string VALUE = "value";

        public List<AffiseKeyValue> Convert(string? from)
        {
            var result = new List<AffiseKeyValue>();
            if (string.IsNullOrWhiteSpace(from)) return result;

            try
            {
                var jsonArray = JSONNode.Parse(from).AsArray;
                if (jsonArray is null) return result;
                foreach (var (_, jsonNode) in jsonArray)
                {
                    var jsonObject = jsonNode?.AsObject;
                    if (jsonObject is null) continue;
                    var key = jsonObject[KEY].Value;
                    var value = jsonObject[VALUE].Value;
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        result.Add(new AffiseKeyValue(key, value));
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return result;
        }
    }
}