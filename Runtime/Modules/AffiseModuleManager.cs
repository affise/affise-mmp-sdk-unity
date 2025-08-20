#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using AffiseAttributionLib.AffiseParameters.Factory;
using AffiseAttributionLib.Logs;
using AffiseAttributionLib.Module.Advertising;
using AffiseAttributionLib.Module.Link;
using AffiseAttributionLib.Module.Network;
using AffiseAttributionLib.Module.Phone;
using AffiseAttributionLib.Module.Status;
using AffiseAttributionLib.Module.TikTok;

namespace AffiseAttributionLib.Modules
{
    internal class AffiseModuleManager
    {
        private readonly ILogsManager _logsManager;
        private readonly PostBackModelFactory _postBackModelFactory;

        private readonly Dictionary<AffiseModules, AffiseModule> _modules = new();

        public AffiseModuleManager(ILogsManager logsManager, PostBackModelFactory postBackModelFactory)
        {
            _logsManager = logsManager;
            _postBackModelFactory = postBackModelFactory;
        }

        public void Init(List<object> dependencies)
        {
            InitAffiseModules((module) =>
            {
                module.Dependencies(
                    _logsManager,
                    dependencies,
                    _postBackModelFactory.GetProviders()
                );

                if (module.IsManual == false)
                {
                    ModuleStart(module);
                }
            });
        }

        public void Status(AffiseModules module, OnKeyValueCallback onComplete)
        {
            var affiseModule = GetModule(module);
            if (affiseModule is not null)
            {
                affiseModule.Status(onComplete);
            }
            else
            {
                onComplete.Invoke(new List<AffiseKeyValue>
                {
                    new("error", $"module \"{module.Module()}\" not found")
                });
            }
        }

        public bool ManualStart(AffiseModules module)
        {
            var affiseModule = GetModule(module);
            if (affiseModule is null) return false;
            if (affiseModule.IsManual == false) return false;
            ModuleStart(affiseModule);
            return true;
        }

        public List<AffiseModules> GetModules()
        {
            return _modules.Keys.ToList();
        }

        private void ModuleStart(AffiseModule module)
        {
            module.Start();
            _postBackModelFactory.AddProviders(module.Providers());
        }

        public AffiseModule? GetModule(AffiseModules name)
        {
            return _modules.ContainsKey(name) ? _modules[name] : null;
        }

        public bool HasModule(AffiseModules name)
        {
            return GetModule(name) != null;
        }

        private void InitAffiseModules(Action<AffiseModule> callback)
        {
            var affiseModules = new Dictionary<AffiseModules, AffiseModule>
            {
                { AffiseModules.Advertising, new AdvertisingModule() },
                { AffiseModules.AppsFlyer, new AppsFlyerModule() },
                { AffiseModules.Link, new LinkModule() },
                { AffiseModules.Network, new NetworkModule() },
                { AffiseModules.Phone, new PhoneModule() },
                { AffiseModules.Status, new StatusModule() },
                { AffiseModules.TikTok, new TikTokModule() },
            };

            foreach (var (name, module) in affiseModules)
            {
                _modules[name] = module;
                callback(module);
            }
        }
        
        public T? Api<T>(AffiseModules module)  where T : IAffiseModuleApi
        {
            if (GetModule(module) is T result) return result;
            return default;
        }
    }
}