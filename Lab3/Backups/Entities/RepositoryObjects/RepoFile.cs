using Backups.Abstractions;
using Backups.Models;

namespace Backups.Entities.RepositoryObjects;

public class RepoFile : IRepoFile
{
    public RepoFile(string name, Func<Stream> repoObjStream)
    {
        Name = new MyPath(name);
        RepoObjStream = repoObjStream;
    }

    public MyPath Name { get; }

    public Func<Stream> RepoObjStream { get; }
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}