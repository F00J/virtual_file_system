using VirtualFileSystem.Abstractions;

namespace VirtualFileSystem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //TODO: Remove - just for testing purposes

            Console.Write("Enter command: ");
            string command = Console.ReadLine() ?? string.Empty;

            while (true)
            {
                if (string.IsNullOrWhiteSpace(command))
                {
                    Console.WriteLine("No command provided. Available commands: add, view, move, list, help, info.");
                    return;
                }

                args = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                if (args[0] != "vf")
                {
                    Console.WriteLine("Commands should start with 'vf'.");
                    Console.Write("Enter command: ");
                    command = Console.ReadLine() ?? string.Empty;
                    continue;
                }

                if (args.Length < 2)
                {
                    Console.WriteLine("No command provided after 'vf'. Available commands: add, view, move, list, help, info.");
                    Console.Write("Enter command: ");
                    command = Console.ReadLine() ?? string.Empty;
                    continue;
                }

                CommandDispatcher dispatcher = new CommandDispatcher();
                dispatcher.Dispatch([.. args.Skip(1)]);

                Console.Write("Enter command: ");
                command = Console.ReadLine() ?? string.Empty;

                if (command.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }
        }
    }
}