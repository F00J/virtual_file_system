using VirtualFileSystem.Abstractions;

namespace VirtualFileSystem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //TODO: Remove, just for testing purposes

            Console.Write("Enter command: ");
            string commnad = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(commnad))
            {
                Console.WriteLine("No command provided. Available commands: add, view, move, list, help, info.");
                return;
            }

            args = commnad.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (args.Length == 0)
            {
                Console.WriteLine("No command provided. Available commands: add, view, move, list, help, info.");
                return;
            }

            CommandDispatcher dispatcher = new CommandDispatcher();
            string input = string.Join(' ', args);
            dispatcher.Dispatch(input);
        }
    }
}