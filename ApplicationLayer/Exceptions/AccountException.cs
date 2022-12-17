using System.Dynamic;

namespace ApplicationLayer.Exceptions;

public class AccountException : Exception
{
    private AccountException(string msg)
        : base(msg) { }

    public static AccountException AccountNotFound()
    {
        return new AccountException($"Account not found exception");
    }
}