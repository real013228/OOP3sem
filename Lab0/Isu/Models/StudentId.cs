using System.Runtime.CompilerServices;
using Isu.CustomExceptions;
using Isu.Entities;

namespace Isu.Models;

public class StudentId
{
    private const int MaxIdNum = 900000;
    private const int MinIdNum = 100000;

    public StudentId(int id)
    {
        if (!(id >= MinIdNum && id < MaxIdNum))
        {
            throw StudentIdException.InvalidId(id);
        }

        Id = id;
    }

    private int Id { get; set; }

    public StudentId GetNextId()
    {
        Id++;
        return this;
    }
}