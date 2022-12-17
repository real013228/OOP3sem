using DataAccessLayer.Models;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Levels;

namespace ApplicationLayer.Dto;

public record ManagerDto(ICollection<EmployeeDto> Employees, ReportDto Report, string Name, string Password, Guid ManagerId);