using DataAccessLayer.Models;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Levels;

namespace ApplicationLayer.Dto;

public record ManagerDto(ICollection<Employee> Employees, Report Report, string Name, string Password, Guid ManagerId);