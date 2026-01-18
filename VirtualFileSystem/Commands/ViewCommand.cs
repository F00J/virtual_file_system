using VirtualFileSystem.Enums;
using VirtualFileSystem.Models;
using VirtualFileSystem.Storage;

namespace VirtualFileSystem.Commands
{
    internal class ViewCommand : BaseCommand
    {
        public override Command Command => Command.View;

        public override void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Viewing folder structure from root:");
                VirtualFolder root = FileSystemStorage.LoadRoot();
                ViewFolderStructure(root, "/");
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
                
                Console.WriteLine($"Viewing folder structure from: {path}");
                ViewFolderStructure(folder, path);
            }
            else
            {
                Console.WriteLine("Invalid arguments. Usage: vf view <path>");
            }
        }

        private static void ViewFolderStructure(VirtualFolder folder, string path, string indent = "")
        {
            Console.WriteLine($"{indent}[DIR] {folder.Name}");
            foreach (VirtualFolder subFolder in folder.Folders)
            {
                ViewFolderStructure(subFolder, $"{path.TrimEnd('/')}/{subFolder.Name}", indent + "  ");
            }
        }
    }
}