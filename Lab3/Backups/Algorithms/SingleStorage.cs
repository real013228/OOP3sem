using System.IO.Compression;
using Backups.Abstractions;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms;

public class SingleStorage : IStorageAlgorithm
{
    public IStorage CreateStorage(IReadOnlyCollection<IRepoObject> objects, IRepository repository, IArchiver archiver, string path)
    {
        return archiver.DoArchive(objects, repository, MyPath.PathCombine(path, $@"{DateTime.Now:yyyy-dd-M--HH-mm}.zip"));
    }
}