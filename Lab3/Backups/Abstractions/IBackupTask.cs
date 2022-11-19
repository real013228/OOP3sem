using System.Collections.ObjectModel;
using Backups.Entities;
using Backups.Models;

namespace Backups.Abstractions;

public interface IBackupTask
{
    public MyPath Name { get; }
    public IEnumerable<RestorePoint> RestorePoints { get; }
    public void AddBackupObject(BackupObject obj);
    public void RemoveBackupObject(BackupObject obj);
    public RestorePoint DoJob();
}