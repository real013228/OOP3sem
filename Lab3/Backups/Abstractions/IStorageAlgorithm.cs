using Backups.Entities;

namespace Backups.Abstractions;

public interface IStorageAlgorithm
{
    public IStorage CreateStorage(IReadOnlyCollection<BackupObject> objects, IRepository repository, IArchiver archiver);
}