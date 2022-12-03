using System.Collections.ObjectModel;
using Backups.Abstractions;
using Microsoft.VisualBasic;

namespace Backups.Entities;

public class Backup : IBackup
{
    private readonly List<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new List<RestorePoint>();
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        _restorePoints.Add(restorePoint);
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        if (!_restorePoints.Contains(restorePoint))
            throw new NullReferenceException();
        _restorePoints.Remove(restorePoint);
    }
}