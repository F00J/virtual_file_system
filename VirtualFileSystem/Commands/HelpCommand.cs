using VirtualFileSystem.Enums;

namespace VirtualFileSystem.Commands
{
    internal class HelpCommand : BaseCommand
    {
        public override Command Command => Command.Help;

        public override void Execute(string[] args)
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  add       - Add a new file or folder              - vf add <file|folder> <path>");
            Console.WriteLine("  delete    - Delete a file or folder               - vf delete <path>");
            Console.WriteLine("  view      - View virtual folder tree              - vf view <path>");
            Console.WriteLine("  move      - Move a file or folder                 - vf move <source path> <destination path>");
            Console.WriteLine("  list      - List contents of a folder             - vf list <path>");
            Console.WriteLine("  clearall  - Clear the entire virtual file system  - vf clear");
            Console.WriteLine("  info      - Display application information       - vf info");
            Console.WriteLine("  help      - Display this help message             - vf help");
        }
    }
}