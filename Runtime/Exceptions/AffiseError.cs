#nullable enable
using System;

namespace AffiseAttributionLib.Exceptions
{
    public abstract class AffiseError : Exception
    {
        internal const string MESSAGE_ALREADY_INITIALIZED = "Affise SDK is already initialized";
        public const string UUID_NOT_INITIALIZED = "11111111-1111-1111-1111-111111111111";
        public const string UUID_NO_VALID_METHOD = "22222222-2222-2222-2222-222222222222";
    }
}