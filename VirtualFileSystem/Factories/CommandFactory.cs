using VirtualFileSystem.Enums;
using VirtualFileSystem.Commands;

namespace VirtualFileSystem.Factories
{
    internal static class CommandFactory
    {
        public static BaseCommand? Create(Command command)
        {
            return command switch
            {
                Command.Add => new AddCommand(),
                Command.Delete => new DeleteCommand(),
                Command.View => new ViewCommand(),
                Command.Move => new MoveCommand(),
                Command.List => new ListCommand(),
                Command.Info => new InfoCommand(),
                Command.Help => new HelpCommand(),
                Command.ClearAll => new ClearAllCommand(),
                _ => null
            };
        }
    }
}
