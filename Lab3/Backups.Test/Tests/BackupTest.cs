using Backups.Abstractions;
using Backups.Algorithms;
using Backups.Entities;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test.Tests;

public class BackupTest : IDisposable
{
    private const string Path = @"/mnt/с/TestPath";
    private readonly IStorageAlgorithm _algorithm = new SplitStorage();
    private readonly InMemoryRepository _repository = new InMemoryRepository(Path, new MemoryFileSystem());
    private readonly IArchiver _archiver = new ZipArchiver();
    private readonly BackupObject _obj1 = new BackupObject($@"/mnt/c/TestPath/MegaTest/");
    private readonly BackupObject _obj2 = new BackupObject($@"/mnt/c/TestPath/Test/");

    [Fact]
    public void InMemoryTest()
    {
        _repository.FileSystem.CreateDirectory($@"/mnt/c/TestPath/");
        _repository.FileSystem.CreateDirectory($@"/mnt/c/TestPath/MegaTest/");
        _repository.FileSystem.CreateDirectory($@"/mnt/c/TestPath/Test/");
        _repository.FileSystem.CreateDirectory(@"/mnt/c/TestPath/Task2/");
        _repository.FileSystem.OpenFile(@"/mnt/c/TestPath/Test/FileGayws", FileMode.Create, FileAccess.ReadWrite)
            .Close();
        var task = new BackupTask(_repository, _algorithm, _archiver, "Task2");
        task.AddBackupObject(_obj1);
        task.AddBackupObject(_obj2);
        task.DoJob();
        task.RemoveBackupObject(_obj1);
        task.DoJob();
        Assert.Equal(2, task.RestorePoints.Count());
    }

    public void Dispose()
    {
        _repository.FileSystem.Dispose();
    }
}