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
        string archName = MyPath.PathCombine($"{path}", $"{Guid.NewGuid()}.zip");
        Stream stream = repository.OpenWrite(archName);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Create);
        var visitor = new ZipVisitor(archive);
        foreach (IRepoObject obj in objects)
        {
            obj.Accept(visitor);
        }

        return new ZipStorage(
            new MyPath(MyPath.PathCombine(path, archName)),
            new ZipFolder(visitor.Top, MyPath.PathCombine($"{path}", archName)),
            repository);
    }
}