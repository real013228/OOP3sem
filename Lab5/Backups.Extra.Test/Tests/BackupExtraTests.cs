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

namespace Backups.Extra.Test.Tests;

public class BackupExtraTests
{
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

        // var taskExtra =
            // new BackupTaskExtra(backup, repositoryExtra, algorithm, provider, logger, backupTask);
        // var backupObject1 = new BackupObject(@"TestDir", repository);
        // var backupObject2 = new BackupObject(@"TestFile.txt", repository);
        // taskExtra.AddBackupObject(backupObject1);
        // taskExtra.DoJob();
        // taskExtra.RemoveBackupObject(backupObject1);
        // taskExtra.AddBackupObject(backupObject2);
        // taskExtra.DoJob();
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

        // var taskExtra =
        // new BackupTaskExtra(backup, repositoryExtra, algorithm, provider, logger, backupTask);
        // var backupObject1 = new BackupObject(@"TestDir", repository);
        // var backupObject2 = new BackupObject(@"TestFile.txt", repository);
        // taskExtra.AddBackupObject(backupObject1);
        // taskExtra.DoJob();
        // taskExtra.RemoveBackupObject(backupObject1);
        // taskExtra.AddBackupObject(backupObject2);
        // taskExtra.DoJob();
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

        // IBackupTaskExtra taskExtra =
        // new BackupTaskExtra(backup, repositoryExtra, algorithm, provider, logger, backupTask);
        // var backupObject1 = new BackupObject(@"TestDir", repository);
        // var backupObject2 = new BackupObject(@"TestFile.txt", repository);
        // taskExtra.AddBackupObject(backupObject1);
        // taskExtra.AddBackupObject(backupObject2);
        // taskExtra.DoJob();
        // taskExtra.RemoveBackupObject(backupObject1);
        // taskExtra.DoJob();
        // taskExtra.DoJob();
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

        // IBackupTaskExtra taskExtra =
        // new BackupTaskExtra(backup, repositoryExtra, algorithm, provider, logger, backupTask);
        // var backupObject1 = new BackupObject(@"TestDir", repository);
        // var backupObject2 = new BackupObject(@"TestFile.txt", repository);
        // taskExtra.AddBackupObject(backupObject1);
        // taskExtra.AddBackupObject(backupObject2);
        // taskExtra.DoJob();
        // taskExtra.RemoveBackupObject(backupObject1);
        // taskExtra.DoJob();
        // taskExtra.DoJob();
        // Assert.Equal(2, taskExtra.RestorePoints.Count());
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

        // IBackupExtra backupExtra =
        // new BackupExtra(backup.RestorePoints, new DifferentRestorer(repositoryExtra), backup, new);
        // IBackupTask backupTask = new BackupTask(backup, repository, algorithm, "Task3", provider);

        // var taskExtra =
        //     new BackupTaskExtra(backup, repositoryExtra, algorithm, provider, logger, backupTask);
        // var backupObject1 = new BackupObject(@"TestDir", repository);
        // var backupObject2 = new BackupObject(@"TestFile.txt", repository);
        // taskExtra.AddBackupObject(backupObject1);
        // taskExtra.DoJob();
        // taskExtra.RemoveBackupObject(backupObject1);
        // taskExtra.AddBackupObject(backupObject2);
        // taskExtra.DoJob();
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

        // var taskExtra = new BackupTaskExtra(backupExtra, repositoryExtra, algorithm, provider, logger, backupTask);
        // var backupObject1 = new BackupObject(@"TestDir", repository);
        // var backupObject2 = new BackupObject(@"TestFile.txt", repository);
        // taskExtra.AddBackupObject(backupObject1);
        // taskExtra.DoJob();
        // taskExtra.RemoveBackupObject(backupObject1);
        // taskExtra.AddBackupObject(backupObject2);
        // taskExtra.DoJob();
        // var originalRestorer = new OriginalRestorer();
    }

    [Fact]
    public void LoggingTest()
    {
        const string path = @"C:\Users\real0\OneDrive\real013228\Lab5";
        IBackup backup = new Backup();
        IRepository repository = new Repository(path);
        IRepositoryExtra repositoryExtra = new RepositoryExtra(new MyPath(path), repository);
        IArchiver archiver = new ZipArchiver();
        var algorithm = new SingleStorage<IArchiver>(archiver);
        IDateTimeProvider provider = new DateTimeProvider();
        ILogger logger = new FileLogger(path, repositoryExtra, false);
        IBackupTask backupTask = new BackupTask(backup, repository, algorithm, "Task3", provider);
        IBackupExtra backupExtra = new BackupExtra(backup, logger);
        IBackupTaskExtra backupTaskExtra = new BackupTaskExtra(backupExtra, repositoryExtra, algorithm, provider, logger, backupTask);
        var obj1 = new BackupObject("TestDir", repository);
        var obj2 = new BackupObject("TestFile.txt", repository);
        backupTaskExtra.AddBackupObject(obj1);
        backupTaskExtra.AddBackupObject(obj2);
        backupTaskExtra.DoJob();
        backupTaskExtra.RemoveBackupObject(obj1);
        backupTaskExtra.DoJob();
    }
}