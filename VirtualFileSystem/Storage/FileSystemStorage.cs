using System.Text.Json;
using VirtualFileSystem.Models;

namespace VirtualFileSystem.Storage
{
    internal static class FileSystemStorage
    {
        private const string FileName = "filesystem.json";

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };

        public static VirtualFolder Load()
        {
            try
            {
                if (!File.Exists(FileName))
                {
                    return new VirtualFolder { Name = "root" };
                }

                string json = File.ReadAllText(FileName);
                return JsonSerializer.Deserialize<VirtualFolder>(json) ?? new VirtualFolder { Name = "root" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file system: {ex.Message}");

                return new VirtualFolder { Name = "root" };
            }
        }


        public static void Save(VirtualFolder root)
        {
            try
            {
                string json = JsonSerializer.Serialize(root, JsonOptions);

                File.WriteAllText(FileName, json);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error saving file system: {exception.Message}");
            }
        }
    }
}