using DataAccessLayer.Models.Levels;

namespace DataAccessLayer.Models.Employees;

public class Manager : Employee
{
    public Manager(ICollection<Employee> employees, Level accessLevel, string name, Guid id)
        : base(name, accessLevel, id)
    {
        Employees = employees;
    }

    protected Manager()
    {
        Employees = null!;
    }

    public virtual ICollection<Employee> Employees { get; set; }
}