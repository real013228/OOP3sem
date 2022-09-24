using System.Dynamic;
using Isu.Entities;

namespace Isu.Models;

public class IdGenerator
{
    private StudentId _id;

    public IdGenerator()
    {
        _id = new StudentId(StudentId.MinIdNum);
    }

    public StudentId GetNextId()
    {
        StudentId newId = _id.NextId();
        _id = _id.NextId();
        return newId;
    }
}