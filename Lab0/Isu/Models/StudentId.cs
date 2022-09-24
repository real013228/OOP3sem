using System.Runtime.CompilerServices;
using Isu.CustomExceptions;
using Isu.Entities;

namespace Isu.Models;

public class StudentId
{
    public const int MaxIdNum = 900000;
    public const int MinIdNum = 100000;
    public StudentId(int id)
    {
        if (!(id >= MinIdNum && id < MaxIdNum))
        {
            throw StudentIdException.InvalidId(id);
        }

        Id = id;
    }

    public int Id { get; }

    public StudentId NextId()
    {
        var value = new StudentId(Id + 1);
        return value;
    }
}