﻿using Banks.Entities;

namespace Banks.Abstractions;

public interface IBankAccount
{
    Client ClientAccount { get; }
    decimal TransactionLimit { get; set; }
    decimal Account { get; }
    Guid Id { get; }
    IClock Clock { get; }
    void TakeMoney(decimal value);
    void TopUpMoney(decimal value);
}