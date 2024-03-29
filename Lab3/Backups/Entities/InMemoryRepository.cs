﻿using System.Reflection.Metadata;
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

        var func = new Func<IEnumerable<IRepoObject>>(() =>
        {
            return info
                .EnumerateEntries()
                .Select(dir => GetRepoObject(new MyPath(MyPath.PathCombine($@"{path.PathName}", $@"{dir.Name}"))));
        });

        return new RepoFolder(path.PathName, func);
    }

    public Stream OpenWrite(string path)
    {
        return FileSystem.OpenFile(path, FileMode.Create, FileAccess.ReadWrite);
    }

    public string CreateDirectory(string name)
    {
        UPath ans = MyPath.PathCombine($"{Path.PathName}", name);
        FileSystem.CreateDirectory(ans);
        return ans.ToString();
    }
}