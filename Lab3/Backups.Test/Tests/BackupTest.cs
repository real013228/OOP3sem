using Backups.Abstractions;
using Backups.Algorithms;
using Backups.Entities;
using Xunit;
using Zio.FileSystems;

namespace Backups.Test.Tests;

public class BackupTest : IDisposable
{
    private const string Path = @"/mnt/TestPath";
    private readonly IStorageAlgorithm _algorithm = new SplitStorage();
    private readonly InMemoryRepository _repository = new InMemoryRepository(Path, new MemoryFileSystem());
    private readonly IArchiver _archiver = new ZipArchiver();
    private readonly BackupObject _obj1 = new BackupObject($@"/mnt/c/TestPath/MegaTest/");
    private readonly BackupObject _obj2 = new BackupObject($@"/mnt/c/TestPath/Test/");

    [Fact]
    public void InMemoryTest()
    {
        // var file = new MemoryStream();
        // using Stream memoryStream =
        //     _repository.OpenWrite(@"/mnt/TestPath/Test/FileGayws");
        // memoryStream.CopyTo(file);
        _repository.FileSystem.CreateDirectory($@"/mnt/c/TestPath/Test/");
        _repository.FileSystem.CreateDirectory($@"/mnt/c/TestPath/MegaTest/");

        var task = new BackupTask(_repository, _algorithm, _archiver, "Task2");
        task.AddBackupObject(_obj1);
        task.AddBackupObject(_obj2);
        task.DoJob();
        task.RemoveBackupObject(_obj1);
        task.DoJob();

        // Assert.Equal(2,  _repository.FileSystem);
    }

    public void Dispose()
    {
        _repository.FileSystem.Dispose();
    }
}