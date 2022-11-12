using System.Collections.ObjectModel;
using Backups.Abstractions;
using Microsoft.VisualBasic;

namespace Backups.Entities;

public class Backup : IBackup
{
    private readonly Collection<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new Collection<RestorePoint>();
    }

    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        _restorePoints.Add(restorePoint);
    }
}