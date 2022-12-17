using System.Collections.ObjectModel;
using ApplicationLayer.Dto;
using ApplicationLayer.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Mapping;

public static class AccountMapping
{
    public static AccountDto AsDto(this Account account)
    {
        var msgSources = account.Sources.Select(x => x.AsDto()).ToList();
        return new AccountDto(msgSources, account.AccessLevel, account.Id);
    }
}