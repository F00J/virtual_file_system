using VirtualFileSystem.Enums;
using VirtualFileSystem.Factories;
using VirtualFileSystem.Models;
using VirtualFileSystem.Storage;

namespace VirtualFileSystem.Commands
{
    internal class MoveCommand : BaseCommand
    {
        public override Command Command => Command.Move;

        public override void Execute(string[] args)
        {
            string sourcePath = args[0];
            string destinationPath = args[1];

            if (!File.Exists(sourcePath))
            {
                Console.WriteLine($"Source file does not exist: {sourcePath}");
                return;
            }

            string fileName = Path.GetFileName(sourcePath);
            string destFullPath = Path.Combine(destinationPath, fileName);

            VirtualFolder root = FileSystemStorage.LoadRoot();
            VirtualFolder? destFolder = FileSystemStorage.EnsureFolderPath(root, destinationPath);
            destFolder.Files.Add(VirtualSystemFactory.CreateFile(fileName, destFullPath));

            Console.WriteLine($"File '{fileName}' moved to virtual file system at path: {destFullPath}. Do you want to delete the local file? Type 'YES' to confirm:");
            string? confirmation = Console.ReadLine();
            if (confirmation != null && confirmation.Equals("YES", StringComparison.OrdinalIgnoreCase))
            {
                //File.Delete(sourcePath);
                Console.WriteLine($"Local file '{sourcePath}' has been deleted. (not deleted, just for test...)");
            }

            Console.WriteLine($"File '{fileName}' moved to virtual file system at path: {destFullPath}");
            FileSystemStorage.Save(root);
        }

        public override bool Validate(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid arguments, missing parameters. Usage: vf move <source path> <destination path>");
                return false;
            }
            
            return true;
        }
    }
}