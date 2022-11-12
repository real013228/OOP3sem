namespace Backups.Abstractions;

public interface IStorage
{
    IStorageLifeTime CreateStorageLifeTime();

    // IReadOnlyCollection<IRepoObject> GetRepoObjects();
}