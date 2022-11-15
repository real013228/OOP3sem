using Backups.Models;
using Zio;

namespace Backups.Abstractions;

public interface IRepository
{
    public MyPath Path { get; }
    public IRepoObject GetRepoObject(MyPath path);
    public Stream OpenWrite(string path);
    public string CreateDirectory(string name);
}