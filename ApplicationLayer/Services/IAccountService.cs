using ApplicationLayer.Dto;

namespace ApplicationLayer.Services;

public interface IAccountService
{
    Task<AccountDto> AddAccount(string name, int level, CancellationToken token);
}