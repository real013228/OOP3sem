﻿using Banks.Abstractions;

namespace Banks.Entities.Transactions;

public class IncreaseMoney : ITransaction
{
    public IncreaseMoney(decimal value, Guid account)
    {
        Value = value;
        Account = account;
        Id = Guid.NewGuid();
    }

    public Guid Account { get; }
    public decimal Value { get; }

    public Guid Id { get; }

    public void Accept(ITransactionVisitor visitor)
    {
        visitor.Visit(this);
    }
}