using Backups.Abstractions;
using Backups.Entities;

namespace Backups.Extra.Abstractions;

public interface IBackupExtra : IBackupCleaner, IBackup
{
    void RemoveRestorePoint(RestorePoint restorePoint);
}