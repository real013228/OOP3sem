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

        var algo = new SplitStorage();

        // var algo = new SingleStorage();
        var archiver = new ZipArchiver();
        var backupTask = new BackupTask(repository, algo, archiver, "Task1");

        backupTask.AddBackupObject(new BackupObject(@"Test"));
        backupTask.AddBackupObject(new BackupObject(@"MegaTest2"));
        backupTask.DoJob();
    }
}