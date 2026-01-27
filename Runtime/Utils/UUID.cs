#nullable enable
using System;
using System.Text;

namespace AffiseAttributionLib.Utils
{
    public static class Uuid
    {
        public static string Generate()
        {
            return Guid.NewGuid().ToString();
        }

        public static string ToFakeUuid(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            const int uuidLength = 4 * 8;

            var sb = new StringBuilder(uuidLength);
            while (sb.Length < uuidLength)
            {
                sb.Append(text);
            }

            sb.Length = uuidLength;

            var baseString = sb.ToString();

            var uuid1 = baseString.Substring(0, 8);
            var uuid2 = baseString.Substring(8, 4);
            var uuid3 = baseString.Substring(12, 4);
            var uuid4 = baseString.Substring(16, 4);
            var uuid5 = baseString.Substring(20, 12);

            return $"{uuid1}-{uuid2}-{uuid3}-{uuid4}-{uuid5}";
        }

        internal static string Sign(this string text, SignType type)
        {
            var suffix = type.Suffix();
            if (suffix is null || text.Length < suffix.Length)
            {
                return text;
            }

            return text.Substring(0, text.Length - suffix.Length) + suffix;
        }

        internal enum SignType
        {
            RANDOM,
            INSTALL_TIME,
            PERSISTENT,
        }

        private static string? Suffix(this SignType type)
        {
            return type switch
            {
                SignType.RANDOM => "00",
                SignType.INSTALL_TIME => "01",
                SignType.PERSISTENT => "02",
                _ => null
            };
        }
    }
}