using System.Collections.Generic;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events.Parameters
{
    public interface IPredefinedParameter<out T>
    {
        /**
         * Add predefined [parameter] with [value] of string
         */
        T AddPredefinedParameter(PredefinedString parameter, string value);

        /**
         * Add predefined [parameter] with [value] of long
         */
        T AddPredefinedParameter(PredefinedLong parameter, long value);

        /**
         * Add predefined [parameter] with [value] of float
         */
        T AddPredefinedParameter(PredefinedFloat parameter, float value);

        /**
         * Add predefined [parameter] with [value] of List string
         */
        T AddPredefinedParameter(PredefinedListString parameter, List<string> value);

        /**
         * Add predefined [parameter] with [value] of JSONObject
         */
        T AddPredefinedParameter(PredefinedObject parameter, JSONObject value);

        /**
         * Add predefined [parameter] with [value] of List<JSONObject>
         */
        T AddPredefinedParameter(PredefinedListObject parameter, List<JSONObject> value);
        
        /**
         * Add predefined ListGroup [value] of List<PredefinedGroup>
         */
        // PredefinedGroup
        // T AddPredefinedListGroup(List<PredefinedGroup> value);
    }
}