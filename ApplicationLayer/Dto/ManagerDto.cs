namespace ApplicationLayer.Dto;

public record ManagerDto(ICollection<EmployeeDto> Employees, ReportDto Report, string Name, string Password, Guid ManagerId);