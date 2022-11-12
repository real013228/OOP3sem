using System.Collections.ObjectModel;
using Backups.Entities;

namespace Backups.Abstractions;

public interface IBackupTask
{
    public void SetBackupObjects(Collection<BackupObject> objects);
    public RestorePoint DoJob();
}