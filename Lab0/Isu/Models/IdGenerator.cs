using System.Dynamic;
using Isu.Entities;

namespace Isu.Models;

public class IdGenerator
{
    private readonly StudentId _id;

    public IdGenerator()
    {
        _id = new StudentId(100000);
    }

    public StudentId NextId()
    {
        return _id.GetNextId();
    }
}