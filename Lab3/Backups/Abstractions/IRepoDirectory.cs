namespace Backups.Abstractions;

public interface IRepoDirectory : IRepoObject
{
    public IEnumerable<IRepoObject> Components();
}