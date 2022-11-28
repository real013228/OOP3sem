using Backups.Abstractions;
using Backups.Entities;
using Backups.Extra.Abstractions;
using Backups.Models;

namespace Backups.Extra.Entities.ExtraEntities;

public class BackupExtra : IBackupExtra
{
    private readonly ICleaner _cleaner;
    private readonly List<RestorePoint> _restorePoints;

    public BackupExtra(IReadOnlyList<RestorePoint> restorePoints, ICleaner cleaner)
    {
        _restorePoints = restorePoints.ToList();
        _cleaner = cleaner;
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        if (_restorePoints.Contains(restorePoint))
            throw new NullReferenceException();
        _restorePoints.Add(restorePoint);
        Clean();
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        if (!_restorePoints.Contains(restorePoint))
            throw new NullReferenceException();
        _restorePoints.Remove(restorePoint);
    }

    public void Clean()
    {
        _cleaner.Clean(RestorePoints, this);
    }
}