using Backups.Models;
using Zio;

namespace Backups.Abstractions;

public interface IRepository
{
    public MyPath Path { get; }
    public IRepoObject GetRepoObject(MyPath path);
    public Stream OpenWrite(string path);
    public string CreateDirectory(string name);

    public int ChildrenCount(string path);

    public static string GetFileName(string path)
    {
        if (path.Contains('/'))
        {
            return path[path.LastIndexOf('/') ..];
        }

        return path.Contains('\\') ? path[path.LastIndexOf('\\') ..] : path;
    }

    public static string PathCombine(string str1, string str2)
    {
        UPath ans = System.IO.Path.Combine(str1, str2);
        return ans.ToString();
    }
}