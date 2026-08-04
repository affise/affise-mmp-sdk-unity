#nullable enable
using System;
using System.Linq;
using AffiseAttributionLib.SimpleJSON;
using UnityEditor;
using UnityEngine;

namespace AffiseAttributionLib.Editor.Utils
{
    internal static class PackageData
    {
        private static JSONObject? Get() => GetPackage();

        private static JSONObject? GetPackage()
        {
            try
            {
                return AssetDatabase
                    .FindAssets("package")
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Where(x => x.EndsWith("package.json"))
                    .Where(x => AssetDatabase.LoadAssetAtPath<TextAsset>(x) is not null)
                    .Select(s => AssetDatabase.LoadAssetAtPath<TextAsset>(s).text)
                    .Select(JSON.Parse)
                    .FirstOrDefault(x => x["name"] == "com.affise.attribution")
                    ?.AsObject;
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public static JSONObject? GetAuthor()
        {
            return Get()?["author"]?.AsObject;
        }

        public static string GetVersion()
        {
            string? ver = Get()?["version"];
            if (ver is null) return "";
            return $"version {ver}";
        }

        public static string GetUrl()
        {
            return GetAuthor()?["url"] ?? "";
        }

        public static string GetEmail()
        {
            return GetAuthor()?["email"] ?? "";
        }
    }
}