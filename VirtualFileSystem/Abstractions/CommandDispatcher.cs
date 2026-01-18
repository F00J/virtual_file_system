using VirtualFileSystem.Commands;
using VirtualFileSystem.Enums;
using VirtualFileSystem.Factories;

namespace VirtualFileSystem.Abstractions
{
    internal class CommandDispatcher
    {
        private readonly Dictionary<Command, BaseCommand> _commands;

        public CommandDispatcher()
        {
            _commands = [];

            foreach (Command cmd in Enum.GetValues<Command>())
            {
                BaseCommand? instance = CommandFactory.Create(cmd);
                
                if (instance != null)
                {
                    _commands[cmd] = instance;
                }
            }
        }

        public void Dispatch(string[] input)
        {
            try
            {
                string commandName = input[0].ToLower();
                string[] args = [.. input.Skip(1)];

                Command? commandEnum = GetCommandFromString(commandName);
                if (commandEnum.HasValue && _commands.TryGetValue(commandEnum.Value, out BaseCommand? commandHandler))
                {
                    if (!commandHandler.Validate(args))
                    {
                        return;
                    }

                    commandHandler.Execute(args);
                }
                else
                {
                    Console.WriteLine($"Unknown command: {commandName}. Type 'help' for a list of commands.");
                }
            }
            catch (Exception exception) 
            {
                Console.WriteLine($"Error executing command: {exception.Message}");
            }
        }

        private static Command? GetCommandFromString(string commandName)
        {
            return commandName.ToLower() switch
            {
                "add" => Command.Add,
                "delete" => Command.Delete,
                "view" => Command.View,
                "move" => Command.Move,
                "list" => Command.List,
                "info" => Command.Info,
                "help" => Command.Help,
                "clearall" => Command.ClearAll,
                _ => null
            };
        }
    }
}
