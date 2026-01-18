using VirtualFileSystem.Enums;

namespace VirtualFileSystem.Commands
{
    internal abstract class BaseCommand
    {
        public abstract Command Command { get; }
        public abstract void Execute(string[] args);

        public virtual bool Validate(string[] args)
        {

            return true;
        }
    }
}