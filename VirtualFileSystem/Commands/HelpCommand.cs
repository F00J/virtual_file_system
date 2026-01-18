using VirtualFileSystem.Enums;

namespace VirtualFileSystem.Commands
{
    //Commands should be with examles

    internal class HelpCommand : BaseCommand
    {
        public override Command Command => Command.Help;

        public override void Execute(string[] args)
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  add   - Add a new file or folder");
            Console.WriteLine("  delete- Delete a file or folder");
            Console.WriteLine("  view  - View details of a file or folder");
            Console.WriteLine("  move  - Move a file or folder to a different location");
            Console.WriteLine("  list  - List contents of a folder");
            Console.WriteLine("  info  - Display application information");
            Console.WriteLine("  help  - Display this help message");
        }
    }
}