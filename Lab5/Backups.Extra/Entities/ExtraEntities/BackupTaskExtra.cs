using Backups.Abstractions;
using Backups.Entities;
using Backups.Extra.Abstractions;
using Backups.Models;

namespace Backups.Extra.Entities.ExtraEntities;

public class BackupTaskExtra : IBackupTaskExtra
{
    private readonly IRepositoryExtra _repository;
    private readonly IStorageAlgorithm _algorithm;
    private readonly List<BackupObject> _objects;
    private readonly IDateTimeProvider _provider;
    private readonly ILogger _logger;
    private readonly IBackupExtra _backup;
    private readonly IBackupTask _backupTask;

    public BackupTaskExtra(IBackupExtra backup, IRepositoryExtra repository, IStorageAlgorithm algorithm, IDateTimeProvider provider, ILogger logger, IBackupTask backupTask)
    {
        Name = new MyPath(backupTask.Name.PathName);
        _backupTask = backupTask;
        _logger = logger;
        _repository = repository;
        _algorithm = algorithm;
        _provider = provider;
        _objects = new List<BackupObject>();
        _backup = backup;
    }

    public MyPath Name { get; }
    public IEnumerable<RestorePoint> RestorePoints => _backup.RestorePoints;

    public void AddBackupObject(BackupObject obj)
    {
        _backupTask.AddBackupObject(obj);
    }

    public void RemoveBackupObject(BackupObject obj)
    {
        _backupTask.RemoveBackupObject(obj);
    }

    public RestorePoint DoJob()
    {
        RestorePoint restorePoint = _backupTask.DoJob();
        _backup.AddRestorePoint(restorePoint);
        _logger.Log($"{_algorithm}");
        return restorePoint;
    }

    public void Clean(IEnumerable<RestorePoint> restorePoints, ICleaner cleaner)
    {
        string log = $"Cleaning restore points: \n";
        _logger.Log(log);
    }
}