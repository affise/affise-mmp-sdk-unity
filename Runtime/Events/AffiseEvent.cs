#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Events.Parameters;
using SimpleJSON;

namespace AffiseAttributionLib.Events
{
    public abstract class AffiseEvent : IPredefinedParameter<AffiseEvent>
    {
        /**
         * Event predefined parameters
         */
        private readonly Dictionary<string, object> _predefinedParameters = new();

        /**
         * Is first for user, default false
         */
        private bool _firstForUser;

        /**
         * Serialize event to JSONObject
         *
         * @return JSONObject
         */
        public abstract JSONNode Serialize();

        /**
         * Name of event
         *
         * @return name
         */
        public abstract string GetName();

        /**
         * Category of event
         *
         * @return category
         */
        public abstract string GetCategory();

        /**
         * User data
         *
         * @return userData
         */
        public abstract string? GetUserData();

        /**
         * Is first for user, default false
         *
         * @return is first for user or not
         */
        public bool IsFirstForUser()
        {
            return _firstForUser;
        }

        internal void SetFirstForUser(bool firstForUser)
        {
            _firstForUser = firstForUser;
        }

        /**
         * Add predefined [parameter] with [value] of string to event
         */
        public AffiseEvent AddPredefinedParameter(PredefinedString parameter, string value)
        {
            _predefinedParameters.Add(parameter.ToValue(), value);
            return this;
        }

        /**
         * Add predefined [parameter] with [value] of List string to event
         */
        public AffiseEvent AddPredefinedParameter(PredefinedListString parameter, List<string> value)
        {
            _predefinedParameters.Add(parameter.ToValue(), value);
            return this;
        }

        /**
         * Add predefined [parameter] with [value] of long to event
         */
        public AffiseEvent AddPredefinedParameter(PredefinedLong parameter, long value)
        {
            _predefinedParameters.Add(parameter.ToValue(), value);
            return this;
        }

        /**
         * Add predefined [parameter] with [value] of float to event
         */
        public AffiseEvent AddPredefinedParameter(PredefinedFloat parameter, float value)
        {
            _predefinedParameters.Add(parameter.ToValue(), value);
            return this;
        }

        /**
         * Add predefined [parameter] with [value] of JSONObject to event
         */
        public AffiseEvent AddPredefinedParameter(PredefinedObject parameter, JSONObject value)
        {
            _predefinedParameters.Add(parameter.ToValue(), value);
            return this;
        }

        /**
         * Add predefined [parameter] with [value] of List<JSONObject> to event
         */
        public AffiseEvent AddPredefinedParameter(PredefinedListObject parameter, List<JSONObject> value)
        {
            _predefinedParameters.Add(parameter.ToValue(), value);
            return this;
        }

        /**
         * Add predefined ListGroup [value] of List<PredefinedGroup> to event
         */
        // PredefinedGroup
        // public AffiseEvent AddPredefinedListGroup(List<PredefinedGroup> value)
        // {
        //     if (!_predefinedParameters.ContainsKey(PredefinedGroup.NAME))
        //     {
        //         _predefinedParameters.Add(PredefinedGroup.NAME, new List<Dictionary<string, object>>());
        //     }
        //     if (_predefinedParameters[PredefinedGroup.NAME] is not List<Dictionary<string, object>> groups)
        //     {
        //         return this;
        //     }
        //     foreach (var group in value)
        //     {
        //         groups.Add(group.GetPredefinedParameters());
        //     }
        //     return this;
        // }

        /**
         * Store and send this events
         */
        public void Send() {
            Affise.SendEvent(this);
        }
        
        /**
         * Send this event now
         */
        public void SendNow(OnSendSuccessCallback success, OnSendFailedCallback failed) {
            Affise.SendEventNow(this, success, failed);
        }

        /**
         * Get map of predefined parameter
         *
         * @return map of predefined parameter
         */
        internal Dictionary<string, object> GetPredefinedParameters() => _predefinedParameters;
        
        protected void AddRawParameters<T>(Dictionary<string, T>? parameters)
        {
            if (parameters is null) return;
            foreach (var (key, value) in parameters)
            {
                if (key is null) continue;
                if (value is null) continue;
                _predefinedParameters.Add($"{PredefinedConstants.PREFIX}{key}", value);
            }
        }
    }
}