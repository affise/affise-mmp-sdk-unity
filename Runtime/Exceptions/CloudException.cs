#nullable enable
using System;

namespace AffiseAttributionLib.Exceptions
{
    internal class CloudException : Exception
    {
        public string URL { get; }
        public Exception Exception { get; }
        public int Attempts { get; }
        public bool Retry { get; }

        public CloudException(string url, Exception exception, int attempts, bool retry = false)
        {
            URL = url;
            Exception = exception;
            Attempts = attempts;
            Retry = retry;
        }
    }
}