using Backups.Entities;

namespace Backups.Abstractions;

public interface IArchiver
{
    IStorage DoArchive(IReadOnlyCollection<IRepoObject> objects, IRepository repository, string path);
}