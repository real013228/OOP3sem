using System.IO.Compression;
using Backups.Abstractions;
using Backups.Entities.ZipObjects;
using Backups.Models;

namespace Backups.Entities;

public class ZipArchiver : IArchiver
{
    private readonly List<IRepoObject> _forest;

    public ZipArchiver()
    {
        _forest = new List<IRepoObject>();
    }

    public IStorage DoArchive(IReadOnlyCollection<IRepoObject> objects,  IRepository repository, string path)
    {
        Stream stream = repository.OpenWrite(path);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Create);
        var visitor = new ZipVisitor(archive);
        foreach (IRepoObject obj in objects)
        {
            obj.Accept(visitor);
        }

        // return new ZipStorage(new MyPath(" "), new ZipFolder(new List<IZipObject>(), " "), new Repository(" "));
        return new ZipStorage(
            new MyPath(IRepository.PathCombine(path, $"{DateTime.Now:yyyy-dd-M--HH-mm-ss}.zip")),
            new ZipFolder(visitor.Top, IRepository.PathCombine(path, $@"{DateTime.Now:yyyy-dd-M--HH-mm-ss}.zip")),
            repository);
    }
}