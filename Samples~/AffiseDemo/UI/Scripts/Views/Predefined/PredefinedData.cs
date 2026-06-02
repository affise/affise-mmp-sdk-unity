#nullable enable

using System;

namespace AffiseAppDemo
{
    public enum PredefinedType
    {
        PREDEFINED_FLOAT,
        PREDEFINED_LONG,
        PREDEFINED_STRING,
    }

    [Serializable]
    public sealed class PredefinedData
    {
        public PredefinedType predefinedType;
        public string predefined = string.Empty;
        public object? data;

        public PredefinedData()
        {
        }

        public PredefinedData(PredefinedType predefinedType, string predefined, object? data)
        {
            this.predefinedType = predefinedType;
            this.predefined = predefined;
            this.data = data;
        }
    }
}
