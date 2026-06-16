#nullable enable
using AffiseAttributionLib.Modules;

namespace AffiseAttributionLib.Module.Google
{
    internal class AffiseGoogle : AffiseModuleApiWrapper<IAffiseGoogleApi>, IAffiseModuleGoogleApi
    {
        protected override AffiseModules Module => AffiseModules.Google;

        public bool HasModule() => IsModuleInit;
    }
}
