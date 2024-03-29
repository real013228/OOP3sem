﻿using Banks.Abstractions;

namespace Banks.ConsoleApplicationHandlers.CancelTransaction;

public class CancelTransactionHandler : IConsoleApplicationHandler
{
    public CancelTransactionHandler(ICentralBank mainCentralBank)
    {
        MainCentralBank = mainCentralBank;
    }

    public ICentralBank MainCentralBank { get; }

    public void SetLessResponsibilitiesHandler(IHandlerLessResponsibilities handler)
    {
        throw new NotImplementedException();
    }

    public void SetNextHandler(IConsoleApplicationHandler nextHandler)
    {
        throw new NotImplementedException();
    }

    public void Handle(char key)
    {
        if (key == '5')
        {
            Console.WriteLine("\nPlease enter Id of transaction");
            string? transactionId = Console.ReadLine();
            if (transactionId != null && Guid.TryParse(transactionId, out Guid outValue))
            {
                MainCentralBank.CancelTransaction(outValue);
                Console.WriteLine("\nTransaction has been cancelled successfully!");
            }

            Console.WriteLine("Bad id of transaction, try again later!");
        }
    }
}