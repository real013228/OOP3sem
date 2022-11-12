using Backups.Abstractions;
using Backups.Models;

namespace Backups.Entities.RepositoryObjects;

public class RepoFolder : IRepoDirectory
{
    public RepoFolder(string name, Func<IEnumerable<IRepoObject>> components)
    {
        Components = components;
        Name = new MyPath(name);
    }

    public Func<IEnumerable<IRepoObject>> Components { get; }

    public MyPath Name { get; }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}