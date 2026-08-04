using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Internal;
using AffiseAttributionLib.Logs;
using AffiseAttributionLib.SimpleJSON;
using UnityEngine;

namespace AffiseAttributionLib.Storages
{
    internal class InternalEventsStorageImpl : IEventsStorage
    {
        private readonly ILogsManager _logsManager;

        public InternalEventsStorageImpl(ILogsManager logsManager)
        {
            _logsManager = logsManager;
        }

        public void SaveEvent(string key, SerializedEvent affiseEvent)
        {
            var filePath = Path.Join(GetEventsDirectory(key), affiseEvent.Id);
            File.WriteAllText(filePath, affiseEvent.Data.ToString());
        }

        public List<SerializedEvent> GetEvents(string key)
        {
            var result = new List<SerializedEvent>();
            var directoryInfo = new DirectoryInfo(GetEventsDirectory(key));
            var storeDate = DateTime.UtcNow.AddMilliseconds(-InternalEventsParams.INTERNAL_EVENTS_STORE_TIME);
            var allFiles = directoryInfo.GetFiles().ToList();
            var deleteFiles = allFiles.Where(file => file.LastWriteTimeUtc < storeDate);
            var files = allFiles
                .Where(file => file.LastWriteTimeUtc >= storeDate)
                .OrderBy(file => file.LastWriteTimeUtc)
                .TakeLast(InternalEventsParams.INTERNAL_EVENTS_SEND_COUNT);

            foreach (var deleteFile in deleteFiles)
            {
                deleteFile.Delete();
            }

            foreach (var file in files)
            {
                try
                {
                    var rawData = File.ReadAllText(file.FullName);
                    var data = JSON.Parse(rawData);
                    result.Add(new SerializedEvent(file.Name, data));
                }
                catch (Exception e)
                {
                    file.Delete();
                    _logsManager.AddSdkError(e);
                }
            }

            return result;
        }

        public void DeleteEvent(string key, IEnumerable<string> ids)
        {
            var list = ids.ToList();
            if (list.Count == 0) return;
            var directoryInfo = new DirectoryInfo(GetEventsDirectory(key));
            foreach (var file in directoryInfo.GetFiles())
            {
                if (list.Contains(file.Name))
                {
                    file.Delete();
                }
            }
        }

        public void Clear()
        {
            Directory.Delete(Path.Join(Application.persistentDataPath, InternalEventsParams.INTERNAL_EVENTS_DIR_NAME), true);
        }

        public bool HasEvent(string key)
        {
            var directoryInfo = new DirectoryInfo(GetEventsDirectory(key));
            var allFiles = directoryInfo.GetFiles();
            return allFiles.Length > 0;
        }

        private string GetEventsDirectory(string key)
        {
            var eventDir = Path.Join(Application.persistentDataPath, InternalEventsParams.INTERNAL_EVENTS_DIR_NAME);

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

            return keyDir;
        }
    }
}
