using ApplicationLayer.Dto;

namespace ApplicationLayer.Services;

public interface IMessageSourceService
{
    Task<MessageSourceDto> AddMessageSource(Guid accountId, string type, string name, Guid employeeId, CancellationToken token);
}