using DataAccessLayer.Models.Levels;

namespace DataAccessLayer.Models.Employees;

public class Manager : Employee
{
    public Manager(ICollection<Employee> employees, string name, string password, Guid id)
        : base(name, password, id)
    {
        Employees = employees;
    }

    protected Manager()
    {
        Employees = null!;
    }

    public virtual ICollection<Employee> Employees { get; set; }
}