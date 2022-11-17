using System.Collections.ObjectModel;
using Backups.Algorithms;
using Backups.Entities;
using Zio;

namespace Backups.Test;

public static class ProgramBackupTest
{
    public static void BackupTest()
    {
        var repository = new Repository(@"C:\Users\real0\OneDrive\real013228\Lab3");

        // var algo = new SingleStorage();
        var algo = new SplitStorage();
        var archiver = new ZipArchiver();
        var backupTask = new BackupTask(new Backup(), repository, algo, archiver, "TaskFinalSplit");

        backupTask.AddBackupObject(new BackupObject(@"Test", repository));
        backupTask.AddBackupObject(new BackupObject(@"MegaTest2", repository));
        backupTask.AddBackupObject(new BackupObject(@"asasas.txt", repository));
        backupTask.DoJob();
    }
}