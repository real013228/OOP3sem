using ApplicationLayer.Dto;

namespace ApplicationLayer.Services;

public interface IHandleMessage
{
    Task<MessageDto> Handle(Guid sessionId, Guid messageId, CancellationToken token);
}