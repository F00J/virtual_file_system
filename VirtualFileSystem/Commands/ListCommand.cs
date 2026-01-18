using VirtualFileSystem.Enums;
using VirtualFileSystem.Models;
using VirtualFileSystem.Storage;

namespace VirtualFileSystem.Commands
{
    internal class ListCommand : BaseCommand
    {
        public override Command Command => Command.List;

        public override void Execute(string[] args)
        {
            VirtualFolder root = FileSystemStorage.LoadRoot();

            string path = args.Length == 0 ? "root" : args[0];

            if (!FileSystemStorage.Exists(path))
            {
                Console.WriteLine($"Path does not exist: {path}");
                return;
            }

            VirtualFolder? folder = FileSystemStorage.ResolveFolderByPath(root, path);

            if (folder == null)
            {
                Console.WriteLine($"Path is not a folder: {path}");
                return;
            }

            Console.WriteLine($"Listing contents of folder: {folder.FullPath}");
            
            ListContents(folder);
        }

        private static void ListContents(VirtualFolder folder)
        {
            foreach (VirtualFolder subFolder in folder.Folders)
            {
                Console.WriteLine($"[DIR]  {subFolder.Name}");
            }

            foreach (VirtualFile file in folder.Files)
            {
                Console.WriteLine($"[FILE] {file.Name}");
            }
        }
    }
}