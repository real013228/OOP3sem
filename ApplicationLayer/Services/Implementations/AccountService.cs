using System.Collections.ObjectModel;
using ApplicationLayer.Dto;
using ApplicationLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Levels;
using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly DatabaseContext _context;

    public AccountService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<AccountDto> AddAccount(string name, int level, CancellationToken token)
    {
        var accessLevel = new Level(level);
        var sources = new Collection<MessageSource>();
        var account = new Account(sources, accessLevel, Guid.NewGuid());
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync(token);
        return account.AsDto();
    }
}