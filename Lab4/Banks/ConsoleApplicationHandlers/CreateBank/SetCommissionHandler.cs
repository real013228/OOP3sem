﻿using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class SetCommissionHandler : ISetBankParameter
{
    private ISetBankParameter? _nextHandler;

    public SetCommissionHandler(ICentralBank mainCentralBank, Bank.BankBuilder builder)
    {
        MainCentralBank = mainCentralBank;
        Builder = builder;
    }

    public ICentralBank MainCentralBank { get; }
    public Bank.BankBuilder Builder { get; }

    public void SetNextHandler(ISetBankParameter nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(string value)
    {
        Builder.WithCommission(decimal.Parse(value));
        while (true)
        {
            Console.WriteLine("Please set credit limit for your new bank");
            string? creditLimit = Console.ReadLine();
            if (creditLimit != null && decimal.TryParse(creditLimit, out decimal outNum))
            {
                Console.WriteLine($"Credit limit has been set successfully! New value is {creditLimit}");
                _nextHandler?.Handle(creditLimit);
                break;
            }

            Console.WriteLine("Try Again");
        }
    }
}