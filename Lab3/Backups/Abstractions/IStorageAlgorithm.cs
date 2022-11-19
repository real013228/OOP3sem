using Backups.Entities;

namespace Backups.Abstractions;

public interface IStorageAlgorithm
{
    public IStorage CreateStorage(IReadOnlyList<IRepoObject> objects, IRepository repository, string path);
}