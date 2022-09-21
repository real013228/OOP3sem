using System.Runtime.CompilerServices;
using Isu.CustomExceptions;

namespace Isu.Models;

public class StudentId
{
    private const int MaxIdNum = 900000;
    private const int MinIdNum = 100000;
    private readonly int _id;

    public StudentId(int value)
    {
        if (value < 0 || value >= MaxIdNum)
        {
            throw new InvalidIdValueException(value.ToString());
        }

        var id = new IdGenerator(value);
        _id = id.GetId();
    }

    public int GetId()
    {
        return _id;
    }

    public int GetMinimumId()
    {
        return MinIdNum;
    }
}