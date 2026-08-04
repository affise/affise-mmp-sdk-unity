#nullable enable
using AffiseAttributionLib.Events.Property;
using AffiseAttributionLib.Utils;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events
{
    public abstract class NativeEvent: AffiseEvent
    {
        private readonly string? _userData;
        private readonly long _timeStampMillis;

        protected NativeEvent(string? userData, long timeStampMillis)
        {
            _userData = userData;
            _timeStampMillis = timeStampMillis;
        }
        
        protected NativeEvent(string? userData): this(userData: userData, timeStampMillis: Timestamp.New())
        {}
        
        protected NativeEvent(): this(userData: null)
        {}

        /**
         * Category of event
         *
         * @return category
         */
        public override string GetCategory() => "native";
        
        /**
         * User data
         *
         * @return userData
         */
        public override string? GetUserData() => _userData;
        
        /**
         * Serialize Event to JSONObject
         *
         * @return JSONObject of Event
         */
        public override JSONNode Serialize() => SerializeBuilder().Build();

        protected virtual AffisePropertyBuilder SerializeBuilder()
        {
            return new AffisePropertyBuilder()
                .Init(GetName())
                .Add(AffiseProperty.TIMESTAMP, _timeStampMillis);
        }
    }
}