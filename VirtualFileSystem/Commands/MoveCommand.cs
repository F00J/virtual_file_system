using VirtualFileSystem.Enums;

namespace VirtualFileSystem.Commands
{
    internal class MoveCommand : BaseCommand
    {
        public override Command Command => Command.Move;

        public override void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}