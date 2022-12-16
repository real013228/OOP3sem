using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Dto;

public record ReportDto(ICollection<BaseMessage> Messages, int Count);