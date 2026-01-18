namespace VirtualFileSystem.Models
{
    public class VirtualFolder
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public List<VirtualFile> Files { get; set; } = [];
        public List<VirtualFolder> Folders { get; set; } = [];
    }
}