using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Levels;

namespace ApplicationLayer.Dto;

public record ManagerDto(ICollection<Employee> Employees, string Name, string Password, Guid Id);