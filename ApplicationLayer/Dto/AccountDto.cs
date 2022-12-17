using DataAccessLayer.Models.Levels;

namespace ApplicationLayer.Dto;

public record AccountDto(ICollection<MessageSourceDto> Sources, Level AccessLevel, Guid AccountId);