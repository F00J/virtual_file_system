using System.Text.Json;
using VirtualFileSystem.Factories;
using VirtualFileSystem.Models;

namespace VirtualFileSystem.Storage
{
    public static class FileSystemStorage
    {
        private const string FileName = "file_system.json";

        private static readonly JsonSerializerOptions JsonOptions = new()
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
                    return VirtualSystemFactory.CreateRoot();
                }

                string json = File.ReadAllText(FileName);

                return JsonSerializer.Deserialize<VirtualFolder>(json)?? VirtualSystemFactory.CreateRoot();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error loading file system: {exception.Message}");
                return VirtualSystemFactory.CreateRoot();
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

        public static VirtualFolder? ResolveFolderByPath(VirtualFolder root, string fullPath)
        {
            return FlattenFolders(root)
                .FirstOrDefault(f =>
                    f.FullPath.Equals(fullPath, StringComparison.OrdinalIgnoreCase));
        }

        public static VirtualFile? ResolveFileByPath(VirtualFolder root, string fullPath)
        {
            return FlattenFiles(root)
                .FirstOrDefault(f =>
                    f.FullPath.Equals(fullPath, StringComparison.OrdinalIgnoreCase));
        }

        public static bool Exists(string fullPath)
        {
            VirtualFolder root = LoadRoot();

            return FlattenFolders(root)
                       .Any(f => f.FullPath.Equals(fullPath, StringComparison.OrdinalIgnoreCase))
                   || FlattenFiles(root)
                       .Any(f => f.FullPath.Equals(fullPath, StringComparison.OrdinalIgnoreCase));
        }

        public static VirtualFolder EnsureFolderPath(VirtualFolder root, string fullPath)
        {
            string[] parts = fullPath
                .Trim('/')
                .Split('/', StringSplitOptions.RemoveEmptyEntries);

            VirtualFolder current = root;

            for (int i = 1; i < parts.Length; i++)
            {
                string folderName = parts[i];

                VirtualFolder? next = current.Folders
                    .FirstOrDefault(f => f.Name.Equals(folderName, StringComparison.OrdinalIgnoreCase));

                if (next == null)
                {
                    string full = current.FullPath == "root"
                        ? $"root/{folderName}"
                        : $"{current.FullPath}/{folderName}";

                    next = new VirtualFolder
                    {
                        Name = folderName,
                        FullPath = full
                    };

                    current.Folders.Add(next);
                }

                current = next;
            }

            return current;
        }

        public static void DeletePath(string fullPath)
        {
            VirtualFolder root = LoadRoot();

            if (fullPath.Equals("root", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            VirtualFolder? folder = ResolveFolderByPath(root, fullPath);
            if (folder != null)
            {
                RemoveFolder(root, folder);
                Save(root);
                return;
            }

            VirtualFile? file = ResolveFileByPath(root, fullPath);
            if (file != null)
            {
                VirtualFolder? parent = ResolveFolderByPath(
                    root,
                    GetParentPath(fullPath)
                );

                parent?.Files.Remove(file);
                Save(root);
            }
        }

        #region Helper Methods

        private static void RemoveFolder(VirtualFolder root, VirtualFolder target)
        {
            foreach (VirtualFolder folder in FlattenFolders(root))
            {
                if (folder.Folders.Remove(target))
                {
                    return;
                }
            }
        }


        private static IEnumerable<VirtualFolder> FlattenFolders(VirtualFolder root)
        {
            yield return root;

            foreach (VirtualFolder folder in root.Folders)
            {
                foreach (VirtualFolder sub in FlattenFolders(folder))
                {
                    yield return sub;
                }
            }
        }

        private static IEnumerable<VirtualFile> FlattenFiles(VirtualFolder root)
        {
            foreach (VirtualFolder folder in FlattenFolders(root))
            {
                foreach (VirtualFile file in folder.Files)
                {
                    yield return file;
                }
            }
        }


        private static string GetParentPath(string fullPath)
        {
            int index = fullPath.LastIndexOf('/');
            return index <= 0 ? "root" : fullPath[..index];
        }

        #endregion
    }
}