#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using AffiseAttributionLib.Editor.Modules;
using AffiseAttributionLib.SimpleJSON;
using UnityEditor;
using UnityEngine;

namespace AffiseAttributionLib.Editor.Utils
{
    internal class ModulesData
    {
        private const string KEY_NAME = "name";
        private const string KEY_MODULE = "module";
        private const string KEY_VERSION = "version";
        private const string KEY_ANDROID = "android";
        private const string KEY_IOS = "ios";
        private const string KEY_COMMENT = "comment";
        private const string KEY_ENABLED = "enabled";
        
        // Editor/Resources/AffiseModules.json
        private const string ASSET_NAME = "AffiseModules";
        
        private static JSONArray? GetJson()
        {
            try
            {
                return AssetDatabase
                    .FindAssets(ASSET_NAME)
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Where(x => x.EndsWith($"{ASSET_NAME}.json"))
                    .Where(x => AssetDatabase.LoadAssetAtPath<TextAsset>(x) is not null)
                    .Select(s => AssetDatabase.LoadAssetAtPath<TextAsset>(s).text)
                    .Select(JSON.Parse)
                    .FirstOrDefault()
                    ?.AsArray;
            }
            catch (Exception e)
            {
                // ignored
                Debug.LogError($"Affise Modules config: {e}");
            }

            return null;
        }

        public static List<Modules.Module> Get()
        {
            var json = GetJson();
            var result = new List<Modules.Module>();
            if (json is null) return result;

            foreach (var (_, jsonNode) in json)
            {
                var name = jsonNode[KEY_NAME]?.Value;
                if (name is null) continue;
                var androidNode = jsonNode[KEY_ANDROID];
                var iosNode = jsonNode[KEY_IOS];
                var hasAndroid = GetModule(androidNode);
                var hasIos = GetModule(iosNode);

                // Data for modules settings config file
                var module = new Modules.Module()
                {
                    name = name,
                    android = hasAndroid?.Enabled ?? false,
                    ios = hasIos?.Enabled ?? false,
                    androidModule = hasAndroid != null,
                    iosModule = hasIos != null,
                };

                result.Add(module);
            }

            return result;
        }

        public static Tuple<string?, string?> GetVersion()
        {
            string? androidVersion = null;
            string? iosVersion = null;

            var data = GetDependencies();
            foreach (var (_, (android, ios)) in data)
            {
                if (androidVersion is not null && iosVersion is not null)
                {
                    return new Tuple<string?, string?>(androidVersion, iosVersion);
                }

                if (android is not null)
                {
                    androidVersion = android.Version;
                }

                if (ios is not null)
                {
                    iosVersion = ios.Version;
                }
            }

            return new Tuple<string?, string?>(androidVersion, iosVersion);
        }

        public static Dictionary<string, Tuple<ModuleData?, ModuleData?>> GetDependencies()
        {
            var json = GetJson();
            var result = new Dictionary<string, Tuple<ModuleData?, ModuleData?>>();
            if (json is null) return result;
            foreach (var (_, value) in json)
            {
                var name = value[KEY_NAME]?.Value;
                if (name is null) continue;
                var androidModule = GetModule(value[KEY_ANDROID]);
                var iosModule = GetModule(value[KEY_IOS]);
                result.Add(name, new Tuple<ModuleData?, ModuleData?>(androidModule, iosModule));
            }

            return result;
        }

        private static bool HasModule(JSONNode? node)
        {
            if (node == null) return false;
            if (!node.IsObject) return false;
            var json = node.AsObject;
            var module = json[KEY_MODULE]?.Value;
            if (string.IsNullOrWhiteSpace(module)) return false;
            var version = json[KEY_VERSION]?.Value;
            if (string.IsNullOrWhiteSpace(version)) return false;
            return true;
        }

        private static ModuleData? GetModule(JSONNode? node)
        {
            if (node == null) return null;
            if (!node.IsObject) return null;
            var json = node.AsObject;
            var module = json[KEY_MODULE]?.Value;
            var version = json[KEY_VERSION]?.Value;

            if (module is null) return null;
            if (version is null) return null;

            var comment = json[KEY_COMMENT]?.Value;
            var enabled = json[KEY_ENABLED]?.AsBool ?? false;

            if (string.IsNullOrWhiteSpace(comment))
            {
                comment = null;
            }

            return new ModuleData
            {
                Comment = comment,
                Module = module,
                Version = version,
                Enabled = enabled
            };
        }

        private static IReadOnlyDictionary<string, Tuple<string?, string?, string?>> GetModuleComment()
        {
            var json = GetJson();
            var result = new Dictionary<string, Tuple<string?, string?, string?>>();
            if (json is null) return result;

            foreach (var (_, value) in json)
            {
                var name = value[KEY_NAME]?.Value;
                var comment = value[KEY_COMMENT]?.Value;
                if (name is null) continue;
                var androidModule = GetModule(value[KEY_ANDROID]);
                var iosModule = GetModule(value[KEY_IOS]);

                result.Add(name, new Tuple<string?, string?, string?>(
                    comment,
                    androidModule?.Comment,
                    iosModule?.Comment
                ));
            }

            return result;
        }

        public static IReadOnlyDictionary<string, string?> GetModulesComment()
        {
            var result = new Dictionary<string, string?>();

            var comments = GetModuleComment();

            foreach (var (moduleName, (comment, androidComment, iosComment)) in comments)
            {
                result[moduleName] = CreateTooltip(comment, androidComment, iosComment);
            }

            return result;
        }

        private static string? CreateTooltip(
            string? comment,
            string? androidComment,
            string? iosComment
        )
        {
            var result = "";
            if (!string.IsNullOrWhiteSpace(comment)) result = comment;

            if (!string.IsNullOrWhiteSpace(androidComment))
            {
                if (!string.IsNullOrWhiteSpace(result)) result += "\n\n";
                result += $"Android:\n{androidComment}";
            }

            if (!string.IsNullOrWhiteSpace(iosComment))
            {
                if (!string.IsNullOrWhiteSpace(result)) result += "\n\n";
                result += $"iOS:\n{iosComment}";
            }

            if (string.IsNullOrWhiteSpace(result)) return null;
            return result;
        }
    }
}