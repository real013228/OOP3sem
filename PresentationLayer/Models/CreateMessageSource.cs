namespace PresentationLayer.Models;

public record CreateMessageSource(Guid AccountId, string Type, string Name, Guid EmployeeId);