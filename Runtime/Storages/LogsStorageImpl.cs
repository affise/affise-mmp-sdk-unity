using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AffiseAttributionLib.Logs;
using AffiseAttributionLib.SimpleJSON;
using UnityEngine;

namespace AffiseAttributionLib.Storages
{
    internal class LogsStorageImpl : ILogsStorage
    {
        private const string LOGS_DIR_NAME = "affise-logs";
        private const int LOGS_MAX_COUNT = 5;

        public void SaveLog(string key, string subKey, SerializedLog log)
        {
            //Create log dir
            var logsDir = GetLogsDirectory(key, subKey);

            //Delete old logs
            var directoryInfo = new DirectoryInfo(GetLogsDirectory(key, subKey));
            var deleteFiles = directoryInfo.GetFiles().ToList()
                .OrderBy(file => file.LastWriteTimeUtc)
                .SkipLast(LOGS_MAX_COUNT - 1);

            foreach (var deleteFile in deleteFiles)
            {
                deleteFile.Delete();
            }

            //Save logs
            //File for log
            var logFileDir = Path.Join(logsDir, subKey);

            //Write log to file
            File.WriteAllText(logFileDir, log.Data.ToString());
        }

        public List<SerializedLog> GetLogs(string key, List<string> subKeys)
        {
            var result = new List<SerializedLog>();
            foreach (var subKey in subKeys)
            {
                var directoryInfo = new DirectoryInfo(GetLogsDirectory(key, subKey));
                var files = directoryInfo.GetFiles()
                    .OrderBy(file => file.LastWriteTimeUtc);

                foreach (var file in files)
                {
                    try
                    {
                        var rawData = File.ReadAllText(file.FullName);
                        var data = JSON.Parse(rawData).AsObject;
                        result.Add(new SerializedLog(file.Name, subKey, data));
                    }
                    catch (Exception)
                    {
                        Debug.LogError("Log SDK read error");
                        file.Delete();
                    }
                }
            }

            return result;
        }

        public void DeleteLogs(string key, IEnumerable<string> subKeys, IEnumerable<string> ids)
        {
            if (ids.Count() == 0) return;
            var directoryInfos = subKeys.Select(subKey => new DirectoryInfo(GetLogsDirectory(key, subKey)));
            foreach (var info in directoryInfos)
            {
                foreach (var fileInfo in info.GetFiles())
                {
                    if (ids.Contains(fileInfo.Name))
                    {
                        fileInfo.Delete();
                    }
                }
            }
        }

        public void Clear()
        {
            Directory.Delete(Path.Join(Application.persistentDataPath, LOGS_DIR_NAME), true);
        }

        public bool HasLogs(string key, List<string> subKeys)
        {
            foreach (var subKey in subKeys)
            {
                var directoryInfo = new DirectoryInfo(GetLogsDirectory(key, subKey));
                var files = directoryInfo.GetFiles();
                return files.Count() > 0;
            }

            return false;
        }

        private string GetLogsDirectory(string key, string subKey = null)
        {
            var eventDir = Path.Join(Application.persistentDataPath, LOGS_DIR_NAME);

            if (!Directory.Exists(eventDir))
            {
                Directory.CreateDirectory(eventDir);
            }

            if (string.IsNullOrEmpty(key))
            {
                return eventDir;
            }

            var keyDir = Path.Join(eventDir, key);
            if (!Directory.Exists(keyDir))
            {
                Directory.CreateDirectory(keyDir);
            }

            if (string.IsNullOrEmpty(subKey))
            {
                return keyDir;
            }

            var subKeyDir = Path.Join(keyDir, subKey);
            if (!Directory.Exists(subKeyDir))
            {
                Directory.CreateDirectory(subKeyDir);
            }

            return subKeyDir;
        }
    }
}