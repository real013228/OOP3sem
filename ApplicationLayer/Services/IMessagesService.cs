using ApplicationLayer.Dto;
using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Services;

public interface IMessagesService
{
    Task<List<MessageDto>> GetMessagesAsync(Guid employeeId, Guid sessionId, CancellationToken token);
    Task<MessageDto> CreateMessage(Guid sourceId, string type, string text, string theme, CancellationToken token);
}