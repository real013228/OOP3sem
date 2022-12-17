using System.Collections.ObjectModel;
using ApplicationLayer.Dto;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Employees;
using Microsoft.VisualBasic;

namespace ApplicationLayer.Mapping;

public static class AccountMapping
{
    public static AccountDto AsDto(this Account account)
    {
        var msgSources = new Collection<MessageSourceDto>();
        foreach (var msgSrc in account.Sources)
        {
            msgSources.Add(msgSrc.AsDto());
        }
        return new AccountDto(msgSources, account.AccessLevel, account.Id);
    }
}