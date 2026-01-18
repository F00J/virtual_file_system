using VirtualFileSystem.Enums;

namespace VirtualFileSystem.Commands
{
    internal class ViewCommand : BaseCommand
    {
        public override Command Command => Command.View;

        public override void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}