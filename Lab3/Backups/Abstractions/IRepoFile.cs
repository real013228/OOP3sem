namespace Backups.Abstractions;

public interface IRepoFile : IRepoObject
{
    public Func<Stream> RepoObjStream { get; }
}