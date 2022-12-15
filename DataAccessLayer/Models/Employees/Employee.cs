using DataAccessLayer.Models.Levels;

namespace DataAccessLayer.Models.Employees;

public abstract class Employee
{
    public Employee(string name, Level accessLevel, Guid id)
    {
        Name = name;
        AccessLevel = accessLevel;
        Id = id;
    }

    protected Employee()
    {
        AccessLevel = null!;
        Name = null!;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Level AccessLevel { get; set; }
}