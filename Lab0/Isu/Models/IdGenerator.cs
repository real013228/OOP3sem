using System.Dynamic;
using Isu.Entities;

namespace Isu.Models;

public class IdGenerator
{
    private readonly int _id;

    public IdGenerator(int tableNum)
    {
        var id = new StudentId(tableNum);
        _id = tableNum + id.GetMinimumId();
    }

    public int GetId()
    {
        return _id;
    }
}