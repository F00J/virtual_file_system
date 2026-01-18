using VirtualFileSystem.Enums;
using VirtualFileSystem.Storage;

namespace VirtualFileSystem.Commands
{
    internal class ClearAllCommand : BaseCommand
    {
        public override Command Command => Command.ClearAll;
        public override void Execute(string[] args)
        {
            Console.WriteLine("Are you sure you want to clear all data in the virtual file system? This action cannot be undone. Type 'YES' to confirm:");
            string? confiramtion = Console.ReadLine();

            while (confiramtion != null)
            {
                if (confiramtion.Equals("YES", StringComparison.OrdinalIgnoreCase))
                {
                    FileSystemStorage.ClearAll();
                    Console.WriteLine("All data in the virtual file system has been cleared.");
                    break;
                }

                if (confiramtion.Equals("NO", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Clear all operation cancelled.");
                    return;
                }

                Console.WriteLine("Invalid input. Please type 'YES' to confirm or 'NO' to cancel:");
                confiramtion = Console.ReadLine();
            }
        }
    }
}