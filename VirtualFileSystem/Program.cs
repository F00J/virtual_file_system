using VirtualFileSystem.Abstractions;

namespace VirtualFileSystem
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No command provided. Usage: vf <command> [options]");
                return 1;
            }

            if (args[0] != "vf")
            {
                Console.WriteLine("Commands should start with 'vf'.");
                return 1;
            }

            if (args.Length < 2)
            {
                Console.WriteLine("No subcommand provided. Available commands: add, clearall, delete, view, move, list, help, info.");
                return 1;
            }

            CommandDispatcher dispatcher = new CommandDispatcher();
            dispatcher.Dispatch([.. args.Skip(1)]);

            return 0;
        }
    }
}