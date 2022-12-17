using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Dto;

public record MessageDto(string MessageValue, DateTime Time, MessageStatus Status, Guid MessageId);