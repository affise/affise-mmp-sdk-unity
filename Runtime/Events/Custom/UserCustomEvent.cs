#nullable enable


using System.Collections.Generic;

namespace AffiseAttributionLib.Events.Custom
{
    public class UserCustomEvent : NativeEvent
    {
        private readonly string _eventName;
        private readonly string? _category;

        public UserCustomEvent(string eventName, string? category = null)
            : base()
        {
            _eventName = eventName;
            _category = category;
        }
        
        public UserCustomEvent(string eventName, string? userData, string? category = null)
            : base(userData)
        {
            _eventName = eventName;
            _category = category;
        }
        
        public UserCustomEvent(string eventName, string? userData, long timeStampMillis, string? category)
            : base(userData, timeStampMillis)
        {
            _eventName = eventName;
            _category = category;
        }

        public override string GetName() => _eventName;

        public override string GetCategory()
        {
            return _category ?? base.GetCategory();
        }

        public AffiseEvent InternalAddRawParameters<T>(Dictionary<string, T>? parameters)
        {
            AddRawParameters(parameters);
            return this;
        }
    }
}