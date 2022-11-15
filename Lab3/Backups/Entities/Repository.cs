using Backups.Abstractions;
using Backups.Entities.RepositoryObjects;
using Backups.Models;
using Zio;

namespace Backups.Entities;

public class Repository : IRepository
{
    public Repository(string path)
    {
        Path = new MyPath(path);
    }

    public MyPath Path { get; }

    public IRepoObject GetRepoObject(MyPath path)
    {
        if (File.Exists($@"{Path.PathName}\{path.PathName}"))
        {
            return new RepoFile(
                System.IO.Path.GetFileName(path.PathName),
                () => File.OpenRead($@"{Path.PathName}\{path.PathName}"));
        }

        if (!Directory.Exists($@"{Path.PathName}\{path.PathName}")) throw new NullReferenceException();
        var info = new DirectoryInfo($@"{Path.PathName}\{path.PathName}");

        // var list = new List<IRepoObject>();
        // foreach (var dir in info.GetFileSystemInfos())
        // {
        //     list.Add(GetRepoObject(new MyPath($@"{path.PathName}\{dir.Name}")));
        // }
        var list = info.GetFileSystemInfos().Select(dir => GetRepoObject(new MyPath($@"{path.PathName}\{dir.Name}")))
            .ToList();
        IEnumerable<IRepoObject> Func() => list as IEnumerable<IRepoObject>;
        return new RepoFolder(System.IO.Path.GetFileName(path.PathName), Func);
    }

    public Stream OpenWrite(string path)
    {
        return File.Open(path, FileMode.Create);
    }

    public string CreateDirectory(string name)
    {
        Directory.CreateDirectory(System.IO.Path.Combine($"{Path.PathName}", $"{name}"));
        return System.IO.Path.Combine($"{Path.PathName}", $"{name}");
    }
}