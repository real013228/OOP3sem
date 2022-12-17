using ApplicationLayer.Dto;
using DataAccessLayer.Models.Employees;

namespace ApplicationLayer.Mapping;

public static class EmployeeMapping
{
    public static EmployeeDto AsDto(this Employee employee)
        => new EmployeeDto(employee.EmployeeName, employee.EmployeePassword);
}
