using DataAccessLayer.Models.Levels;

namespace DataAccessLayer.Models.Employees;

public class Manager : Employee
{
    public Manager(ICollection<Employee> employees, string name, string password, Guid managerId, Report report)
        : base(name, password, managerId)
    {
        Employees = employees;
        Report = report;
    }

    protected Manager()
    {
        Employees = null!;
        Report = null!;
    }

    public virtual ICollection<Employee> Employees { get; set; }
    public virtual Report Report { get; set; }
}