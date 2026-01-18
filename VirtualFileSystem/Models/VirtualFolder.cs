namespace VirtualFileSystem.Models
{
    internal class VirtualFolder
    {
        public string Name { get; set; }
        public List<VirtualFile> Files { get; set; } = [];
        public List<VirtualFolder> Folders { get; set; } = [];
    }
}