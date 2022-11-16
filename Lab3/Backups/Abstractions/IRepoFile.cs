namespace Backups.Abstractions;

public interface IRepoFile : IRepoObject
{
    public Stream RepoObjStream();
}