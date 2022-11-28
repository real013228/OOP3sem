using Backups.Entities;

namespace Backups.Extra.Abstractions;

public interface ICleaner
{
    void Clean(IEnumerable<RestorePoint> restorePoints, IBackupExtra backupExtra);
}