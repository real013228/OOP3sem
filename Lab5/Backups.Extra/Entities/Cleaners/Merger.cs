using Backups.Abstractions;
using Backups.Entities;
using Backups.Extra.Abstractions;
using Backups.Models;

namespace Backups.Extra.Entities.Cleaners;

public class Merger : ICleaner
{
    private readonly IStorageAlgorithm _algorithmExtra;
    private readonly IRepositoryExtra _repository;
    private readonly string _path;
    private readonly IDateTimeProvider _provider;

    public Merger(IStorageAlgorithm algorithmExtra, IRepositoryExtra repository, string path, IDateTimeProvider provider)
    {
        _algorithmExtra = algorithmExtra;
        _repository = repository;
        _path = path;
        _provider = provider;
    }

    public void Clean(IEnumerable<RestorePoint> restorePoints, IBackupExtra backupExtra)
    {
        if (!restorePoints.Any())
            throw new NullReferenceException();
        var restorePointsList = restorePoints.ToList();
        RestorePoint restorePoint = restorePointsList.OrderBy(rp => rp.CreationDate).Reverse().First();
        var storageLifeTimes = new List<IStorageLifeTime>();
        var backupObjects = restorePoint.BackupObjects.ToList();
        var repoObjects = backupObjects.Select(obj => _repository.GetRepoObject(new MyPath(obj.Descriptor))).ToList();
        foreach (RestorePoint obj in restorePointsList)
        {
            IStorageLifeTime storageLifeTime = obj.Storage.CreateStorageLifeTime();
            foreach (BackupObject bo in obj.BackupObjects)
            {
                if (backupObjects.Contains(bo)) continue;
                backupObjects.Add(bo);
                storageLifeTimes.Add(storageLifeTime);
                IRepoObject? repoObj =
                    storageLifeTime.RepoObjects.FirstOrDefault(x =>
                        x.Name.PathName == MyPath.GetFileName(bo.Descriptor));
                if (repoObj == null)
                    throw new NullReferenceException();
                repoObjects.Add(repoObj);
            }
        }

        var deletableResPoints = restorePointsList.ToList();
        string newRestorePointName = $"restore-point-{Guid.NewGuid()}";
        var newRestorePoint = new RestorePoint(backupObjects, _algorithmExtra.CreateStorage(repoObjects, _repository, _path), _provider.GetTime(), newRestorePointName);
        foreach (IStorageLifeTime storageLifeTime in storageLifeTimes)
        {
            storageLifeTime.Dispose();
        }

        string pathName = MyPath.PathCombine(_path, newRestorePointName);
        string newPath = _repository.CreateDirectory(pathName);

        _algorithmExtra.CreateStorage(repoObjects, _repository, newPath);
        var remover = new Remover(_repository, MyPath.GetFileName(_path));
        remover.Clean(deletableResPoints, backupExtra);
        backupExtra.AddRestorePoint(newRestorePoint);
    }
}