using System.IO.Compression;
using Backups.Abstractions;
using Backups.Entities.RepositoryObjects;
using Backups.Models;

namespace Backups.Entities.ZipObjects;

public class ZipFolder : IZipObject
{
    public ZipFolder(IEnumerable<IZipObject> children, string path)
    {
        Children = children;
        Name = new MyPath(path);
    }

    public IEnumerable<IZipObject> Children { get; }
    public MyPath Name { get; }

    public IRepoObject CreateRepoObject(ZipArchiveEntry zipArchiveEntry)
    {
        IEnumerable<IRepoObject> Func()
        {
            var arch = new ZipArchive(zipArchiveEntry.Open(), ZipArchiveMode.Read);
            var res = new List<IRepoObject>();
            foreach (ZipArchiveEntry child in arch.Entries)
            {
                IZipObject obj = Children.First(x => x.Name.PathName == child.Name);
                res.Add(obj.CreateRepoObject(child));
            }

            return res;
        }

        return new RepoFolder(Name.PathName, Func);
    }
}