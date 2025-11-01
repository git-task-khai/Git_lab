using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace ProjetcGit.Classes
{
    public static class JsonStorage
    {
        public static void SaveToJson<T>(T data, string filePath)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при сохранении JSON: " + ex.Message);
            }
        }

        public static T LoadFromJson<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return Activator.CreateInstance<T>();

                var serializer = new JavaScriptSerializer();
                var json = File.ReadAllText(filePath);
                return serializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при загрузке JSON: " + ex.Message);
                return Activator.CreateInstance<T>();
            }
        }
    }
}
