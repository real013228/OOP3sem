using Backups.Abstractions;
using Backups.Models;

namespace Backups.Entities.RepositoryObjects;

public class RepoFile : IRepoFile
{
    private readonly Func<Stream> _repoObjStream;
    public RepoFile(string name, Func<Stream> repoObjStream)
    {
        Name = new MyPath(name);
        _repoObjStream = repoObjStream;
    }

    public MyPath Name { get; }

    public Stream RepoObjStream()
    {
        return _repoObjStream();
    }

    public void Accept(IArchiverVisitor archiverVisitor)
    {
        archiverVisitor.Visit(this);
    }
}