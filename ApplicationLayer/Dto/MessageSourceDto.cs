using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Dto;

public record MessageSourceDto(string Login, ICollection<BaseMessage> Messages);