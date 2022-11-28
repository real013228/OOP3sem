namespace Backups.Entities;

public class RestorePointEqualityComparer : IEqualityComparer<RestorePoint>
{
    public bool Equals(RestorePoint? x, RestorePoint? y)
    {
        return x!.Name == y!.Name && x.CreationDate == y.CreationDate;
    }

    public int GetHashCode(RestorePoint obj)
    {
        return obj.GetHashCode();
    }
}