using DataAccessLayer.Models.Levels;
using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Dto;

public record AccountDto(ICollection<MessageSource> Sources, Level AccessLevel);