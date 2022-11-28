using Backups.Abstractions;
using Backups.Entities;
using Backups.Extra.Abstractions;
using Backups.Models;

namespace Backups.Extra.Entities.ExtraEntities;

public class BackupExtra : IBackupExtra
{
    private readonly IBackup _backup;

    public BackupExtra(IBackup backup, ILogger logger)
    {
        _backup = backup;
        Logger = logger;
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _backup.RestorePoints;
    public ICleaner? Cleaner { get; set; }
    public ILogger Logger { get; set; }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        string log = $"Adding restore point {restorePoint} ...\n";
        Logger.Log(log);
        _backup.AddRestorePoint(restorePoint);
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        string log = $"Removing restore point {restorePoint} ...\n";
        Logger.Log(log);
        _backup.RemoveRestorePoint(restorePoint);
    }

    public void Clean()
    {
        string log = $"Cleaning restore points ...\n";
        Logger.Log(log);
        Cleaner?.Clean(RestorePoints, this);
    }
}