#nullable enable

namespace AffiseAttributionLib.Usecase
{
    public interface IAppUUIDs
    {
        /**
         * Get devise id
         * Return devise id
         */
        string? GetAffiseDeviseId();
        
        /**
         * Get alt devise id
         * Return alt devise id
         */
        string? GetAffiseAltDeviseId();
        
        /**
         * Get random user id
         * Return random user id
         */
        string? GetRandomUserId();

        public const string AFF_DEVICE_ID = "AFF_DEVICE_ID";
        public const string AFF_ALT_DEVICE_ID = "AFF_ALT_DEVICE_ID";
        public const string RANDOM_USER_ID = "random_user_id";
    }
}