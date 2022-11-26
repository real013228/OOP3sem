using System.IO.Compression;
using Backups.Abstractions;
using Backups.Entities.RepositoryObjects;
using Backups.Models;

namespace Backups.Entities.ZipObjects;

public class ZipFile : IZipObject
{
    public ZipFile(string name)
    {
        Name = new MyPath(name);
    }

    public MyPath Name { get; }
    public IRepoObject CreateRepoObject(ZipArchiveEntry zipArchiveEntry)
        => new RepoFile(zipArchiveEntry.Name, zipArchiveEntry.Open);
}