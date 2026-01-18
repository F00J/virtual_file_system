using VirtualFileSystem.Enums;

namespace VirtualFileSystem.Commands
{
    internal class AddCommand : BaseCommand
    {
        public override Command Command => Command.Add;

        public override void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}