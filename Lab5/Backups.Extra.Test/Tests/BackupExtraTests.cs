using Backups.Abstractions;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.Abstractions;
using Backups.Extra.Entities;
using Backups.Extra.Entities.Cleaners;
using Backups.Extra.Entities.Cleaners.Selectors;
using Backups.Extra.Entities.ExtraEntities;
using Backups.Extra.Entities.Loggers;
using Backups.Extra.Entities.Restorers;
using Backups.Models;
using Xunit;
using Xunit.Sdk;
using Zio;
using Zio.FileSystems;

namespace Backups.Extra.Test.Tests;

public class BackupExtraTests : IDisposable
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
        IDateTimeProvider provider = new DateTimeProvider();
        var task = new BackupTask(new Backup(), _repository, algorithm, "Task2", provider);
        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);
        task.DoJob();
        task.RemoveBackupObject(obj1);
        task.DoJob();
        Assert.Equal(2, task.RestorePoints.Count());
    }

    [Fact]
    public void SplitStorageMerge()
    {
        const string path = @"C:\Users\real0\OneDrive\real013228\Lab5";
        IBackup backup = new Backup();
        IRepository repository = new Repository(path);
        IRepositoryExtra repositoryExtra = new RepositoryExtra(new MyPath(path), repository);
        IArchiver archiver = new ZipArchiver();
        var algorithm = new SplitStorage<IArchiver>(archiver);
        IDateTimeProvider provider = new DateTimeProvider();
        ILogger logger = new ConsoleLogger(true);
        IBackupTask backupTask = new BackupTask(backup, repository, algorithm, "Task1", provider);
    }

    [Fact]
    public void SingleStorageMerge()
    {
        const string path = @"C:\Users\real0\OneDrive\real013228\Lab5";
        IBackup backup = new Backup();
        IRepository repository = new Repository(path);
        IRepositoryExtra repositoryExtra = new RepositoryExtra(new MyPath(path), repository);
        IArchiver archiver = new ZipArchiver();
        var algorithm = new SingleStorage<IArchiver>(archiver);
        IDateTimeProvider provider = new DateTimeProvider();
        ILogger logger = new ConsoleLogger(true);
        IBackupTask backupTask = new BackupTask(backup, repository, algorithm, "Task2", provider);
    }

    [Fact]
    public void CountClean()
    {
        const string path = @"C:\Users\real0\OneDrive\real013228\Lab5";
        IBackup backup = new Backup();
        IRepository repository = new Repository(path);
        IRepositoryExtra repositoryExtra = new RepositoryExtra(new MyPath(path), repository);
        IArchiver archiver = new ZipArchiver();
        var algorithm = new SingleStorage<IArchiver>(archiver);
        IDateTimeProvider provider = new DateTimeProvider();
        ILogger logger = new ConsoleLogger(true);
        IBackupTask backupTask = new BackupTask(backup, repository, algorithm, "Task1", provider);
    }

    [Fact]
    public void Clean()
    {
        const string path = @"C:\Users\real0\OneDrive\real013228\Lab5";
        IBackup backup = new Backup();
        IRepository repository = new Repository(path);
        IRepositoryExtra repositoryExtra = new RepositoryExtra(new MyPath(path), repository);
        IArchiver archiver = new ZipArchiver();
        var algorithm = new SingleStorage<IArchiver>(archiver);
        IDateTimeProvider provider = new DateTimeProvider();
        ILogger logger = new ConsoleLogger(true);
        IBackupTask backupTask = new BackupTask(backup, repository, algorithm, "Task1", provider);
    }

    [Fact]
    public void RestoreToDifferentRepository()
    {
        const string path = @"C:\Users\real0\OneDrive\real013228\Lab5";
        IBackup backup = new Backup();
        IRepository repository = new Repository(path);
        IRepositoryExtra repositoryExtra = new RepositoryExtra(new MyPath(path), repository);
        IArchiver archiver = new ZipArchiver();
        var algorithm = new SingleStorage<IArchiver>(archiver);
        IDateTimeProvider provider = new DateTimeProvider();
        ILogger logger = new ConsoleLogger(true);
    }

    [Fact]
    public void RestoreToOriginalRepository()
    {
        const string path = @"C:\Users\real0\OneDrive\real013228\Lab5";
        IBackup backup = new Backup();
        IRepository repository = new Repository(path);
        IRepositoryExtra repositoryExtra = new RepositoryExtra(new MyPath(path), repository);
        IArchiver archiver = new ZipArchiver();
        var algorithm = new SingleStorage<IArchiver>(archiver);
        IDateTimeProvider provider = new DateTimeProvider();
        ILogger logger = new ConsoleLogger(true);
        IBackupTask backupTask = new BackupTask(backup, repository, algorithm, "Task3", provider);
    }

    public void Dispose()
    {
        _repository.FileSystem.Dispose();
    }
}