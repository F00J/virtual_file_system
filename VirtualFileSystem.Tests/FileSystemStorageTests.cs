using VirtualFileSystem.Factories;
using VirtualFileSystem.Models;
using VirtualFileSystem.Storage;

namespace VirtualFileSystem.Tests
{
    [TestFixture]
    public class FileSystemStorageTests
    {
        private VirtualFolder _root;

        [SetUp]
        public void Setup()
        {
            _root = VirtualSystemFactory.CreateRoot();
        }

        [Test]
        public void EnsureFolderPath_CreatesMissingFoldersRecursively()
        {
            VirtualFolder folder = FileSystemStorage.EnsureFolderPath(
                _root,
                "root/test/test/test"
            );

            Assert.That(folder.FullPath, Is.EqualTo("root/test/test/test"));

            VirtualFolder level1 = _root.Folders.Single();
            Assert.Multiple(() =>
            {
                Assert.That(level1.Name, Is.EqualTo("test"));
                Assert.That(level1.FullPath, Is.EqualTo("root/test"));
            });

            VirtualFolder level2 = level1.Folders.Single();
            Assert.That(level2.FullPath, Is.EqualTo("root/test/test"));

            VirtualFolder level3 = level2.Folders.Single();
            Assert.That(level3.FullPath, Is.EqualTo("root/test/test/test"));
        }

        [Test]
        public void EnsureFolderPath_IsIdempotent()
        {
            FileSystemStorage.EnsureFolderPath(_root, "root/a/b/c");
            FileSystemStorage.EnsureFolderPath(_root, "root/a/b/c");

            Assert.That(_root.Folders, Has.Count.EqualTo(1));
            Assert.That(_root.Folders[0].Folders, Has.Count.EqualTo(1));
        }

        [Test]
        public void EnsureFolderPath_UsesExistingFolders()
        {
            VirtualFolder existing = new VirtualFolder
            {
                Name = "a",
                FullPath = "root/a"
            };
            _root.Folders.Add(existing);

            VirtualFolder result = FileSystemStorage.EnsureFolderPath(_root, "root/a/b");

            Assert.Multiple(() =>
            {
                Assert.That(existing.Folders.Single().Name, Is.EqualTo("b"));
                Assert.That(result.FullPath, Is.EqualTo("root/a/b"));
            });
        }

        [Test]
        public void ResolveFolderByPath_FindsFolderByFullPath()
        {
            FileSystemStorage.EnsureFolderPath(_root, "root/a/b/c");

            VirtualFolder? folder = FileSystemStorage.ResolveFolderByPath(_root, "root/a/b/c");

            Assert.That(folder, Is.Not.Null);
            Assert.That(folder!.Name, Is.EqualTo("c"));
        }

        [Test]
        public void File_CreatedWithCorrectFullPath()
        {
            VirtualFolder parent = FileSystemStorage.EnsureFolderPath(_root, "root/docs");

            parent.Files.Add(new VirtualFile
            {
                Name = "readme.txt",
                FullPath = "root/docs/readme.txt"
            });

            VirtualFile? file = FileSystemStorage.ResolveFileByPath(
                _root,
                "root/docs/readme.txt"
            );

            Assert.That(file, Is.Not.Null);
        }

    }
}