using System.Collections.ObjectModel;
using Backups.Abstractions;

namespace Backups.Models;

public class StorageLifeTime : IStorageLifeTime
{
    public StorageLifeTime(IReadOnlyCollection<IRepoObject> repoObjects)
    {
        RepoObjects = repoObjects;
    }

    public IReadOnlyCollection<IRepoObject> RepoObjects { get; }

    public void Dispose()
    {
    }
}