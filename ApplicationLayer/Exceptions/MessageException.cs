using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Exceptions;

public class MessageException : Exception
{
    private MessageException(string msg)
        : base(msg) { }

    public static MessageException MessageNotFound(Guid messageId)
    {
        return new MessageException($"Message: {messageId} not found");
    }
}