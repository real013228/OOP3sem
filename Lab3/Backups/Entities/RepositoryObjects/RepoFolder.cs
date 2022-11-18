using Backups.Abstractions;
using Backups.Models;

namespace Backups.Entities.RepositoryObjects;

public class RepoFolder : IRepoDirectory
{
    private readonly Func<IEnumerable<IRepoObject>> _components;

    public RepoFolder(string name, Func<IEnumerable<IRepoObject>> components)
    {
        _components = components;
        Name = new MyPath(name);
    }

    public MyPath Name { get; }
    public IEnumerable<IRepoObject> Components()
    {
        return _components();
    }

    public void Accept(IArchiverVisitor archiverVisitor)
    {
        archiverVisitor.Visit(this);
    }
}