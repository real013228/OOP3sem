namespace ApplicationLayer.Exceptions;

public class MessageException : Exception
{
    private MessageException(string msg)
        : base(msg) { }

    public static MessageException MessageNotFound()
    {
        return new MessageException($"Message: not found");
    }
}