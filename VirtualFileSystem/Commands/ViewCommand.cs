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

            Console.WriteLine($"Viewing folder structure from: {folder.FullPath}");
            ViewFolderStructure(folder, 0);
        }

        public override bool Validate(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Invalid arguments, too many parameters. Usage: vf view [path]");
                return false;
            }

            return true;
        }

        #region Helper Methods

        private static void ViewFolderStructure(VirtualFolder folder, int indentLevel)
        {
            string indent = new string(' ', indentLevel * 2);

            Console.WriteLine($"{indent}[DIR] {folder.Name}");

            foreach (VirtualFolder subFolder in folder.Folders)
            {
                ViewFolderStructure(subFolder, indentLevel + 1);
            }
        }

        #endregion
    }
}