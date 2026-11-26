namespace AffiseAttributionLib.AffiseParameters.Base
{
    public abstract class PropertyProvider<T> : Provider
    {
        protected abstract T DefaultValue { get; }

        public abstract T Provide();

        public T ProvideWithDefault()
        {
            return Provide() ?? DefaultValue;
        }
    }

    public abstract class StringPropertyProvider : PropertyProvider<string>
    {
        protected override string DefaultValue => "";
    }
    
    public abstract class BooleanPropertyProvider : PropertyProvider<bool?>
    {
        protected override bool? DefaultValue => false;
    }
    
    public abstract class LongPropertyProvider : PropertyProvider<long?>
    {
        protected override long? DefaultValue => 0L;
    }

    public abstract class FloatPropertyProvider : PropertyProvider<float?>
    {
        protected override float? DefaultValue => null;
    }

    public abstract class StringWithParamPropertyProvider : Provider
    {
        private string DefaultValue = "";

        protected abstract string ProvideWithParam(string param);

        public string ProvideWithParamAndDefault(string param) => ProvideWithParam(param) ?? DefaultValue;
    }
}
