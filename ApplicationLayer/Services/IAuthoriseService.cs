using ApplicationLayer.Dto;

namespace ApplicationLayer.Services;

public interface IAuthoriseService
{
    Task<SessionDto> LoginAsync(string name, string password, CancellationToken token);
    Task LogoutAsync(Guid sessionId, CancellationToken token);
}