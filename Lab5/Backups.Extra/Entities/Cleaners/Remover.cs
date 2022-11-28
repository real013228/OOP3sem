using Backups.Entities;
using Backups.Extra.Abstractions;
using Backups.Models;

namespace Backups.Extra.Entities.Cleaners;

public class Remover : ICleaner
{
    private readonly IRepositoryExtra _repository;
    private readonly string _taskName;

    public Remover(IRepositoryExtra repository, string taskName)
    {
        _repository = repository;
        _taskName = taskName;
    }

    public void Clean(IEnumerable<RestorePoint> restorePoints, IBackupExtra backupExtra)
    {
        foreach (RestorePoint restorePoint in restorePoints)
        {
            backupExtra.RemoveRestorePoint(restorePoint);
            _repository.DeleteRestorePoint(restorePoint, MyPath.PathCombine(_repository.Path.PathName, _taskName));
        }
    }
}