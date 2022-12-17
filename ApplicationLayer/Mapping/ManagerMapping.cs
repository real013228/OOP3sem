using System.Collections.ObjectModel;
using ApplicationLayer.Dto;
using DataAccessLayer.Models.Employees;

namespace ApplicationLayer.Mapping;

public static class ManagerMapping
{
    public static ManagerDto AsDto(this Manager manager)
    {
        var employees = manager.Employees.Select(x => x.AsDto()).ToList();
        return new ManagerDto(employees, manager.Report.AsDto(), manager.EmployeeName, manager.EmployeePassword, manager.Id); 
    }
}