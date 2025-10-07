#nullable enable
using System;

namespace AffiseAttributionLib.Exceptions
{
    internal abstract class AffiseError : Exception
    {
        public const string MESSAGE_ALREADY_INITIALIZED = "Affise SDK is already initialized";
    }
}