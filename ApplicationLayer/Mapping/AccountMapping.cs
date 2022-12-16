using ApplicationLayer.Dto;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Employees;

namespace ApplicationLayer.Mapping;

public static class AccountMapping
{
    public static AccountDto AsDto(this Account account)
        => new AccountDto(account.Sources, account.AccessLevel, account.Id);
}