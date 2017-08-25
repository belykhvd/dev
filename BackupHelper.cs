using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace BackupHelper
{
    public class BackupHelper
    {
        private readonly List<BackupableList> backupLists = new List<BackupableList>();

        public readonly string BackupFilesPrefix;
                       
        public BackupHelper(string backupFilesPrefix = "")
        {
            BackupFilesPrefix = backupFilesPrefix;
        }

        public List<object> CreateBackupableList(SerializationType serializationType, string backupFileName, string backupFileExtention = "log")
        {
            var backupList = new BackupableList(serializationType, backupFileName, backupFileExtention);
            backupLists.Add(backupList);
            return backupList.ObjectsList;
        }

        public void Backup()
            => backupLists.ForEach(list => File.WriteAllText($"{BackupFilesPrefix}{list.BackupFileName}.{list.BackupFileExtention}", Serialize(list.ObjectsList, list.SerializationType)));

        private string Serialize(List<object> objectsList, SerializationType serializationType)
        {
            switch (serializationType)
            {
                case SerializationType.Inline:
                    return string.Join("\n", objectsList);
                case SerializationType.JsonSerializable:
                    return JsonConvert.SerializeObject(objectsList, Formatting.Indented);
                default:
                    return "";
            }
        }
        
        private class BackupableList
        {
            public readonly List<object> ObjectsList = new List<object>();                        
            public readonly SerializationType SerializationType;
            public readonly string BackupFileName;
            public readonly string BackupFileExtention;

            public BackupableList(SerializationType serializationType, string backupFileName, string backupFileExtention)
            {
                SerializationType = serializationType;
                BackupFileName = backupFileName;
                BackupFileExtention = backupFileExtention;
            }
        }

        public enum SerializationType
        {
            Inline,
            JsonSerializable
        }
    }
}
