using Backups.Entities;

namespace Backups.Abstractions;

public interface IBackup
{
    public IReadOnlyList<RestorePoint> RestorePoints { get; }
    public void AddRestorePoint(RestorePoint restorePoint);
    public void RemoveRestorePoint(RestorePoint restorePoint);
}