using VirtualFileSystem.Models;
using VirtualFileSystem.Storage;

namespace VirtualFileSystem.Commands
{
    internal class DeleteCommand : BaseCommand
    {
        public override Enums.Command Command => Enums.Command.Delete;
        public override void Execute(string[] args)
        {
            string pathToDelete = args[0];

            VirtualFolder root = FileSystemStorage.LoadRoot();
            VirtualFolder? item = FileSystemStorage.ResolveFolderByPath(root, pathToDelete);

            if (item is null)
            {
                Console.WriteLine($"Path does not exist: {pathToDelete}");
                return;
            }

            if (item.Folders.Count > 0 || item.Files.Count > 0)
            {
                Console.WriteLine($"The folder '{pathToDelete}' is not empty. Are you sure you want to delete it and all its contents? Type 'YES' to confirm:");
                string? confirmation = Console.ReadLine();

                if (confirmation != null)
                {
                    if (confirmation.Equals("YES", StringComparison.OrdinalIgnoreCase))
                    {
                        Storage.FileSystemStorage.DeletePath(item.FullPath);
                        Console.WriteLine($"Folder '{pathToDelete}' and all its contents have been deleted.");
                    }
                    else
                    {
                        Console.WriteLine("Delete operation cancelled.");
                    }
                }
            }
        }

        public override bool Validate(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Invalid arguments, missing path. Usage: vf delete <path>");
                return false;
            }

            if (args.Length > 1)
            {
                Console.WriteLine("Invalid arguments, too many parameters. Usage: vf delete <path>");
                return false;
            }

            if (args[0].Equals("root", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Cannot delete the root folder.");
                return false;
            }

            return true;
        }
    }
}