#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Extensions;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Converter
{
    public class ProvidersToJsonStringConverter : IConverter<List<Provider>, string>
    {
        public string Convert(List<Provider> from)
        {
            var json = new JSONObject();

            foreach (var (key, value) in from.MapProviders())
            {
                json[key.Provider()] = value.ToJsonNode();
            }
            
            var result = new JSONArray();
            result.Add(json);
            return result.ToString();
        }
    }
}