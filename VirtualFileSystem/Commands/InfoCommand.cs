using VirtualFileSystem.Enums;

namespace VirtualFileSystem.Commands
{
    internal class InfoCommand : BaseCommand
    {
        public override Command Command => Command.Info;

        public override void Execute(string[] args)
        {
            Console.WriteLine("Virtual File System Application version v1.0.0.0");
        }
    }
}