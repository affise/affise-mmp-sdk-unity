using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.AffiseParameters.Logs
{
    /**
     * Base Affise log event
     */
    internal class AffiseLog
    {
        public AffiseLogType Name { get; }
        public string Value { get; }

        public AffiseLog(AffiseLogType name, string value)
        {
            Name = name;
            Value = value;
        }

        public class NetworkLog : AffiseLog
        {
            public JSONObject JsonObject { get; }
            
            public NetworkLog(JSONObject jsonObject) : base(AffiseLogType.NETWORK, jsonObject.ToString())
            {
                JsonObject = jsonObject;
            }
        }

        public class DevicedataLog : AffiseLog
        {
            public DevicedataLog(string value) : base(AffiseLogType.DEVICEDATA, value)
            {
            }
        }

        public class UserdataLog : AffiseLog
        {
            public UserdataLog(string value) : base(AffiseLogType.USERDATA, value)
            {
            }
        }

        public class SdkLog : AffiseLog
        {
            public SdkLog(string value) : base(AffiseLogType.SDKLOG, value)
            {
            }
        }
    }
}