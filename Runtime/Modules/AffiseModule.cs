#nullable enable
using System;
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Logs;

namespace AffiseAttributionLib.Modules
{
    public abstract class AffiseModule
    {
        protected ILogsManager? LogsManager;
        private List<object> _dependencies = new();
        private List<Provider> _baseProviders = new();

        public void Dependencies(ILogsManager logsManager, List<object> dependencies, List<Provider> providers)
        {
            LogsManager = logsManager;
            _dependencies = dependencies;
            _baseProviders = providers;
        }

        public abstract void Start();

        public virtual IEnumerable<Provider> Providers()
        {
            return new List<Provider>();
        }

        public virtual void Status(OnKeyValueCallback onComplete)
        {
            onComplete(new List<AffiseKeyValue>
            {
                new("state", "enabled")
            });
        }

        public T? GetProvider<T>() where T : Provider
        {
            return _baseProviders.GetProvider<T>();
        }

        public List<Provider> GetProviders(List<Type> types)
        {
            return _baseProviders.GetProviders(types);
        }

        public List<Provider> GetRequestProviders()
        {
            return _baseProviders.GetRequestProviders();
        }

        protected T? Get<T>() where T : class
        {
            foreach (var obj in _dependencies)
            {
                if (obj is T result)
                {
                    return result;
                }
            }

            return null;
        }
    }
}