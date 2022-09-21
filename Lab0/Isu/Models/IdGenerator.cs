using System.Dynamic;
using Isu.Entities;

namespace Isu.Models;

public class IdGenerator
{
    private readonly int _id;

    public IdGenerator(int tableNum)
    {
        _id = tableNum + StudentId.GetMinimumId();
    }

    public int GetId()
    {
        return _id;
    }
}