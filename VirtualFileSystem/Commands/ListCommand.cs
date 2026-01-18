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
            if (args.Length == 0)
            {
                Console.WriteLine("Listing contents of root folder:");
                VirtualFolder root = FileSystemStorage.LoadRoot();
                ListContents(root, "/");
            }
            else if (args.Length == 1)
            {
                string path = args[0];
                if (!FileSystemStorage.Exists(path))
                {
                    Console.WriteLine($"Path does not exist: {path}");
                    return;
                }
                VirtualFolder? folder = FileSystemStorage.LoadFolder(path);
                if (folder == null)
                {
                    Console.WriteLine($"Path is not a folder: {path}");
                    return;
                }
                Console.WriteLine($"Listing contents of folder: {path}");
                ListContents(folder, path);
            }
            else
            {
                Console.WriteLine("Invalid arguments. Usage: vf list <path>");
            }
        }

        private static void ListContents(VirtualFolder folder, string path)
        {
            foreach (VirtualFolder subFolder in folder.Folders)
            {
                Console.WriteLine($"[DIR]  {path.TrimEnd('/')}/{subFolder.Name}");
            }

            foreach (VirtualFile file in folder.Files)
            {
                Console.WriteLine($"[FILE] {path.TrimEnd('/')}/{file.Name}");
            }
        }
    }
}