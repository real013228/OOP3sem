using Zio;

namespace Backups.Models;

public class MyPath
{
    public MyPath(string path)
    {
        PathName = path;
    }

    public string PathName { get; }
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