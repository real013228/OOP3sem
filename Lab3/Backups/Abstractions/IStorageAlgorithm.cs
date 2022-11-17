using Backups.Entities;

namespace Backups.Abstractions;

public interface IStorageAlgorithm
{
    public IStorage CreateStorage(IReadOnlyCollection<IRepoObject> objects, IRepository repository, IArchiver archiver, string path);
}