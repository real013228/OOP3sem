using System.IO.Compression;
using Backups.Abstractions;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms;

public class SingleStorage<TArchiver> : IStorageAlgorithm
    where TArchiver : IArchiver
{
    private readonly TArchiver _archiver;

    public SingleStorage(TArchiver archiver)
    {
        _archiver = archiver;
    }

    public IStorage CreateStorage(IReadOnlyCollection<IRepoObject> objects, IRepository repository, string path)
    {
        return _archiver.DoArchive(objects, repository, MyPath.PathCombine(path, $@"{DateTime.Now:yyyy-dd-M--HH-mm}.zip"));
    }
}