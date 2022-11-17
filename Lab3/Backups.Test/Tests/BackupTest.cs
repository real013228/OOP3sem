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
    private readonly IArchiver _archiver = new ZipArchiver();
    private readonly InMemoryRepository _repository = new InMemoryRepository(Path, new MemoryFileSystem());

    [Fact]
    public void InMemoryTest()
    {
        IStorageAlgorithm algorithm = new SplitStorage<IArchiver>(_archiver);
        var obj1 = new BackupObject($@"/mnt/c/TestPath/MegaTest/", _repository);
        var obj2 = new BackupObject($@"/mnt/c/TestPath/Test/", _repository);
        _repository.FileSystem.CreateDirectory($@"/mnt/c/TestPath/");
        _repository.FileSystem.CreateDirectory($@"/mnt/c/TestPath/MegaTest/");
        _repository.FileSystem.CreateDirectory($@"/mnt/c/TestPath/Test/");
        _repository.FileSystem.CreateDirectory(@"/mnt/c/TestPath/Task2/");
        _repository.FileSystem.OpenFile(@"/mnt/c/TestPath/Test/FileGayws", FileMode.Create, FileAccess.ReadWrite)
            .Close();
        var task = new BackupTask(new Backup(), _repository, algorithm, "Task2");
        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);
        task.DoJob();
        task.RemoveBackupObject(obj1);
        task.DoJob();
        Assert.Equal(2, task.RestorePoints.Count());
    }

    public void Dispose()
    {
        _repository.FileSystem.Dispose();
    }
}