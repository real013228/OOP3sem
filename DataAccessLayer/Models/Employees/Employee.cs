using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Models.Levels;

namespace DataAccessLayer.Models.Employees;

public abstract class Employee
{
    public Employee(string name, string password, Guid id)
    {
        EmployeeName = name;
        Id = id;
        EmployeePassword = password;
    }

    protected Employee()
    {
        EmployeeName = null!;
        EmployeePassword = null!;
    }

    public Guid Id { get; set; }
    public string EmployeePassword { get; set; }
    public string EmployeeName { get; set; }
}