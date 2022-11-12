namespace Backups.Abstractions;

public interface IStorageLifeTime : IDisposable
{
    public IReadOnlyCollection<IRepoObject> RepoObjects { get; }
}