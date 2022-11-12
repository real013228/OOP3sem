using System.Reflection.Metadata;
using Backups.Abstractions;
using Backups.Entities.RepositoryObjects;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities;

public class InMemoryRepository : IRepository
{
    public InMemoryRepository(string path, IFileSystem fileSystem)
    {
        Path = new MyPath(path);
        FileSystem = fileSystem;
    }

    public MyPath Path { get; }
    public IFileSystem FileSystem { get; }

    public IRepoObject GetRepoObject(MyPath path)
    {
        if (FileSystem.FileExists(path.PathName))
        {
            return new RepoFile(
                path.PathName,
                () => FileSystem.OpenFile(path.PathName, FileMode.Open, FileAccess.ReadWrite));
        }

        if (!FileSystem.DirectoryExists(path.PathName)) throw new NullReferenceException();

        var info = new DirectoryEntry(FileSystem, path.PathName);
        var list = info.EnumerateEntries().Select(dir => GetRepoObject(new MyPath($@"{path.PathName}/{dir.Name}")))
            .ToList();
        IEnumerable<IRepoObject> Func() => list as IEnumerable<IRepoObject>;
        return new RepoFolder(path.PathName, Func);
    }

    public Stream OpenWrite(string path)
    {
        return FileSystem.OpenFile(path, FileMode.Create, FileAccess.ReadWrite);
    }
}