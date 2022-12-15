using DataAccessLayer.Models.Levels;

namespace DataAccessLayer.Models.Employees;

public abstract class Employee
{
    public Employee(string name, Guid id)
    {
        Name = name;
        Id = id;
    }

    protected Employee()
    {
        Name = null!;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}