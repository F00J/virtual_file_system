using VirtualFileSystem.Enums;
using VirtualFileSystem.Models;
using VirtualFileSystem.Storage;

namespace VirtualFileSystem.Commands
{
    internal class AddCommand : BaseCommand
    {
        public override Command Command => Command.Add;

        public override void Execute(string[] args)
        {
            string type = args[0].ToLower();
            string path = args[1];

            bool isFile = type == "file";

            string[] segments = path.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
            string newItemName = segments.Last();
            string parentPath = string.Join('/', segments.Take(segments.Length - 1));

            VirtualFolder root = FileSystemStorage.LoadRoot();
            
            VirtualFolder? parentFolder;
            
            if (parentPath.Equals("root", StringComparison.OrdinalIgnoreCase))
            {
                parentFolder = (VirtualFolder?)root;
            }
            else
            {
                parentFolder = FileSystemStorage.ResolveFolderByPath(root, parentPath);
            }

            if (parentFolder == null)
            {
                Console.WriteLine($"Parent folder '{parentPath}' does not exist.");
                return;
            }

            if (isFile)
            {
                if (parentFolder.Files.Any(f => f.Name.Equals(newItemName, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"A file named '{newItemName}' already exists at path: {parentPath}");
                    return;
                }
                if (parentFolder.Folders.Any(f => f.Name.Equals(newItemName, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"A folder named '{newItemName}' already exists at path: {parentPath}");
                    return;
                }
                parentFolder.Files.Add(new VirtualFile { Name = newItemName });
                Console.WriteLine($"File '{newItemName}' added at path: {path}");
            }
            else
            {
                if (parentFolder.Folders.Any(f => f.Name.Equals(newItemName, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"A folder named '{newItemName}' already exists at path: {parentPath}");
                    return;
                }
                if (parentFolder.Files.Any(f => f.Name.Equals(newItemName, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"A file named '{newItemName}' already exists at path: {parentPath}");
                    return;
                }
                parentFolder.Folders.Add(new VirtualFolder { Name = newItemName });
                Console.WriteLine($"Folder '{newItemName}' added at path: {path}");
            }

            FileSystemStorage.Save(root);
        }

        public override bool Validate(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid arguments. Usage: vf add <file|folder> <path>");
                return false;
            }

            string type = args[0].ToLower();

            if (type != "file" && type != "folder")
            {
                Console.WriteLine("First argument must be either 'file' or 'folder'.");
                return false;
            }

            if (!args[1].StartsWith("root"))
            {
                Console.WriteLine("Path must start with 'root'.");
                return false;
            }

            return true;
        }
    }
}