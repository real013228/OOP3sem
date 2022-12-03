using Backups.Abstractions;
using Backups.Entities;
using Backups.Extra.Abstractions;

namespace Backups.Extra.Entities.Restorers;

public class DifferentRestorer : IRestorer
{
    private readonly IRepositoryExtra _repositoryExtra;

    public DifferentRestorer(IRepositoryExtra repositoryExtra)
    {
        _repositoryExtra = repositoryExtra;
    }

    public void Restore(RestorePoint restorePoint)
    {
        string dirPath = _repositoryExtra.CreateDirectory($"Restored-point-data{restorePoint.Name}");
        IStorageLifeTime storageLifeTime = restorePoint.Storage.CreateStorageLifeTime();
        var visitor = new RestorerVisitor(dirPath, _repositoryExtra);
        foreach (IRepoObject obj in storageLifeTime.RepoObjects)
        {
            obj.Accept(visitor);
        }

        storageLifeTime.Dispose();
    }
}