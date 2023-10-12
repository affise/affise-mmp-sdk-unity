#nullable enable
using System;

namespace AffiseAttributionLib.Exceptions
{
    public class NetworkException : Exception
    {
        public long Code { get; }

        public NetworkException(long code, string message) : base(message)
        {
            Code = code;
        }
    }
}