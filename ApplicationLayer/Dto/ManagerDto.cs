using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Levels;

namespace ApplicationLayer.Dto;

public record ManagerDto(ICollection<Employee> Employees, Level AccessLevel, string Name, Guid Id);