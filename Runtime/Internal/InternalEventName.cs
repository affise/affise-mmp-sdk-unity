namespace AffiseAttributionLib.Internal
{
    internal enum InternalEventName
    {
        SESSION_START,
    }

    internal static class InternalEventNameExt
    {
        public static string ToEventName(this InternalEventName type)
        {
            return type switch
            {
                InternalEventName.SESSION_START => "SessionStart",
                _ => type.ToString()
            };
        }
    }
}
