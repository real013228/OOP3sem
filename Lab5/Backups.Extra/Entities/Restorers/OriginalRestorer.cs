using Backups.Abstractions;
using Backups.Entities;
using Backups.Extra.Abstractions;
using Backups.Extra.Entities.ExtraEntities;
using Backups.Models;

namespace Backups.Extra.Entities.Restorers;

public class OriginalRestorer : IRestorer
{
    public void Restore(RestorePoint restorePoint)
    {
        IStorageLifeTime storageLifeTime = restorePoint.Storage.CreateStorageLifeTime();
        foreach (BackupObject backupObject in restorePoint.BackupObjects)
        {
            string boFullPath = MyPath.PathCombine(backupObject.Repository.Path.PathName, backupObject.Descriptor);
            var newRepo = new RepositoryExtra(new MyPath(boFullPath), backupObject.Repository);
            var visitor = new RestorerVisitor(MyPath.GetFileName(backupObject.Repository.Path.PathName), newRepo);
            foreach (IRepoObject obj in storageLifeTime.RepoObjects)
            {
                if (obj.Name.PathName == backupObject.Descriptor)
                    obj.Accept(visitor);
            }
        }

        storageLifeTime.Dispose();
    }
}