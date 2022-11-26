using System.Collections.ObjectModel;
using Backups.Abstractions;
using Backups.Algorithms;
using Backups.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Zio;

namespace Backups.Test;

public static class ProgramBackupTest
{
    public static void BackupTest()
    {
        var repository = new Repository(@"C:\Users\real0\OneDrive\real013228\Lab3");

        var archiver = new ZipArchiver();
        var algo = new SingleStorage<IArchiver>(archiver);
        IDateTimeProvider provider = new DateTimeProvider();
        var backupTask = new BackupTask(new Backup(), repository, algo,  "TaskFinalSplit2", provider);

        backupTask.AddBackupObject(new BackupObject(@"Test", repository));
        backupTask.AddBackupObject(new BackupObject(@"MegaTest2", repository));
        backupTask.AddBackupObject(new BackupObject(@"asasas.txt", repository));
        backupTask.DoJob();
    }
}