using ApplicationLayer.Dto;
using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Services;

public interface IMessageSourceService
{
    Task<MessageSourceDto> AddMessageSource(Guid accountId, string type, string name, Guid employeeId, CancellationToken token);
}