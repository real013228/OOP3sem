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

    public IStorage CreateStorage(IReadOnlyList<IRepoObject> objects, IRepository repository, string path)
    {
        IStorage storage = _archiver.DoArchive(objects, repository, path);
        return storage;
    }
}