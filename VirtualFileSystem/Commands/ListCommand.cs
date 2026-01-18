using VirtualFileSystem.Enums;

namespace VirtualFileSystem.Commands
{
    internal class ListCommand : BaseCommand
    {
        public override Command Command => Command.List;

        public override void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}