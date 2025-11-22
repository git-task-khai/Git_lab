using System;
using System.IO;
using System.Text.Json;

namespace GitClassLibrary.Classes
{
    public static class JsonStorage
    {
        public static void SaveToJson<T>(T data, string filePath)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при сохранении JSON: " + ex.Message);
            }
        }

        public static T LoadFromJson<T>(string filePath) where T : new()
        {
            try
            {
                if (!File.Exists(filePath))
                    return new T();

                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(json) ?? new T();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при загрузке JSON: " + ex.Message);
                return new T();
            }
        }
    }
}
