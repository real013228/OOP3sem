using System.Collections.ObjectModel;
using Backups.Abstractions;

namespace Backups.Models;

public class StorageLifeTime : IStorageLifeTime
{
    public StorageLifeTime(IReadOnlyList<IRepoObject> repoObjects)
    {
        RepoObjects = repoObjects;
    }

    public IReadOnlyList<IRepoObject> RepoObjects { get; }

    public void Dispose()
    {
    }
}