using VirtualFileSystem.Models;

namespace VirtualFileSystem.Factories
{
    internal static class VirtualSystemFactory
    {
        public static VirtualFolder CreateFolder(string name, string fullPath)
        {
            return new VirtualFolder
            {
                Name = name,
                FullPath = fullPath
            };
        }

        public static VirtualFile CreateFile(string name, string fullPath, string? content = null)
        {
            return new VirtualFile
            {
                Name = name,
                FullPath = fullPath,
                Content = content
            };
        }

        public static VirtualFolder CreateRoot()
        {
            return CreateFolder("root", "root");
        }
    }
}