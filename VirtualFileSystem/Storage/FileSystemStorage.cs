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

        public static VirtualFolder LoadRoot()
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

        public static VirtualFolder? ResolveFolderByPath(VirtualFolder? root, string path)
        {
            string[] parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

            VirtualFolder? current = root;

            for (int i = 1; i < parts.Length; i++)
            {
                current = current?.Folders
                    .FirstOrDefault(f => f.Name.Equals(parts[i], StringComparison.OrdinalIgnoreCase));

                if (current == null)
                {
                    return null;
                }
            }

            return current;
        }

        public static VirtualFolder? LoadFolder(string path)
        {
            string[] segments = path.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
            VirtualFolder currentFolder = LoadRoot();

            foreach (string segment in segments)
            {
                VirtualFolder? nextFolder = currentFolder.Folders.FirstOrDefault(f => f.Name.Equals(segment, StringComparison.OrdinalIgnoreCase));
                if (nextFolder != null)
                {
                    currentFolder = nextFolder;
                }
                else
                {
                    return null;
                }
            }

            return currentFolder;
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

        public static bool Exists(string path)
        {
            string[] segments = path.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);

            VirtualFolder currentFolder = LoadRoot();

            foreach (string segment in segments)
            {
                VirtualFolder? nextFolder = currentFolder.Folders.FirstOrDefault(f => f.Name.Equals(segment, StringComparison.OrdinalIgnoreCase));

                if (nextFolder != null)
                {
                    currentFolder = nextFolder;
                }
                else
                {
                    VirtualFile? file = currentFolder.Files.FirstOrDefault(f => f.Name.Equals(segment, StringComparison.OrdinalIgnoreCase));
                    if (file != null && segment == segments.Last())
                    {
                        return true;
                    }
                    return false;
                }
            }

            return true;
        }

        public static void ClearAll()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error clearing file system: {exception.Message}");
            }
        }
    }
}