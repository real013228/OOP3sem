using Backups.Entities;

namespace Backups.Abstractions;

public interface IArchiver
{
    IStorage DoArchive(IReadOnlyList<IRepoObject> objects, IRepository repository, string path);
}