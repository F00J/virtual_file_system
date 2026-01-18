namespace VirtualFileSystem.Commands
{
    internal class DeleteCommand : BaseCommand
    {
        public override Enums.Command Command => Enums.Command.Delete;
        public override void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}