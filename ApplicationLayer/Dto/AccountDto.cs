using DataAccessLayer.Models.Levels;
using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Dto;

public record AccountDto(ICollection<MessageSourceDto> Sources, Level AccessLevel, Guid AccountId);