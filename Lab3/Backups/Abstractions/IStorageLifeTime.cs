namespace Backups.Abstractions;

public interface IStorageLifeTime : IDisposable
{
    public IReadOnlyList<IRepoObject> RepoObjects { get; }
}