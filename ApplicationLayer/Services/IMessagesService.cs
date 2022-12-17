using ApplicationLayer.Dto;

namespace ApplicationLayer.Services;

public interface IMessagesService
{
    Task<IReadOnlyList<MessageDto>> GetMessagesAsync(Guid employeeId, Guid sessionId, CancellationToken token);
    Task<MessageDto> CreateMessage(Guid sourceId, string type, string text, string theme, CancellationToken token);
}