#nullable enable
using System;
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Logs;
using AffiseAttributionLib.Network.Entity;

namespace AffiseAttributionLib.AffiseParameters.Factory
{
    internal class PostBackModelFactory
    {
        private readonly List<Provider> _providers;

        public PostBackModelFactory(
            List<Provider> providers
        )
        {
            _providers = providers;
        }

        public PostBackModel Create(
            List<SerializedEvent> events,
            List<SerializedLog> logs,
            List<SerializedEvent>? internalEvents = null
        )
        {
            return new PostBackModel(
                parameters: GetProvidersMap(),
                events: events,
                logs: logs,
                internalEvents: internalEvents
            );
        }

        public void AddProviders(IEnumerable<Provider> providers)
        {
            _providers.AddRange(providers);
        }

        public T? GetProvider<T>() where T : Provider
        {
            return _providers.GetProvider<T>();
        }

        public List<Provider> GetProviders(List<Type> types)
        {
            return _providers.GetProviders(types);
        }

        public List<Provider> GetProviders() => _providers;

        public Dictionary<ProviderType, object?> GetProvidersMap()
        {
            return _providers.MapProviders();
        }
    }
}
