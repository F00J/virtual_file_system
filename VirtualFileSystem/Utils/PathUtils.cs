using VirtualFileSystem.Models;

namespace VirtualFileSystem.Helpers
{
    internal class PathUtils
    {
        internal static string BuildFullPath(VirtualFolder parent, string name)
        {
            return parent.FullPath.Equals("root", StringComparison.OrdinalIgnoreCase)
                ? $"root/{name}"
                : $"{parent.FullPath}/{name}";
        }
    }
}
