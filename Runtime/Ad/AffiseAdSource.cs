#nullable enable

namespace AffiseAttributionLib.Ad
{
    public enum AffiseAdSource
    {
        ADMOB,
        ADMOST,
        APPLOVIN_MAX,
        HELIUM_CHARTBOOST,
        IRONSOURCE,
    }
    
    public static class AffiseAdSourceExt
    {
        public static string Type(this AffiseAdSource param)
        {
            return param switch
            {
                AffiseAdSource.ADMOB => "admob",
                AffiseAdSource.ADMOST => "admost",
                AffiseAdSource.APPLOVIN_MAX => "applovin_max",
                AffiseAdSource.HELIUM_CHARTBOOST => "helium_chartboost",
                AffiseAdSource.IRONSOURCE => "ironsource",
            };
        }
    }
}