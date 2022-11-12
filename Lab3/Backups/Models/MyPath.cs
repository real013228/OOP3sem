namespace Backups.Models;

public class MyPath
{
    public MyPath(string path)
    {
        PathName = path;
    }

    public string PathName { get; }
}