namespace Backups.Abstractions;

public interface IStorage
{
    IStorageLifeTime CreateStorageLifeTime();
}