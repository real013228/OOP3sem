using System.Collections.ObjectModel;
using ApplicationLayer.Dto;
using DataAccessLayer.Models.Employees;
using Microsoft.VisualBasic;

namespace ApplicationLayer.Mapping;

public static class ManagerMapping
{
    public static ManagerDto AsDto(this Manager manager)
    {
        var employees = new Collection<EmployeeDto>();
        foreach (var emp in manager.Employees)
        {
            employees.Add(emp.AsDto());
        }
        return new ManagerDto(employees, manager.Report.AsDto(), manager.EmployeeName, manager.EmployeePassword, manager.Id); 
    }
}