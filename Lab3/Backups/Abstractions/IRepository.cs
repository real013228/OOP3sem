using Backups.Models;

namespace Backups.Abstractions;

public interface IRepository
{
    public MyPath Path { get; }
    public IRepoObject GetRepoObject(MyPath path);
    public Stream OpenWrite(string path);
    public static string GetFileName(string path)
    {
        return path.Contains('/') ? path[path.LastIndexOf('/') ..] : path[path.LastIndexOf('\\') ..];
    }
}