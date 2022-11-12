namespace Backups.Abstractions;

public interface IRepoDirectory : IRepoObject
{
    public Func<IEnumerable<IRepoObject>> Components { get; }
}