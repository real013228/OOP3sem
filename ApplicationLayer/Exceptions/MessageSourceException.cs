using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Exceptions;

public class MessageSourceException : Exception
{
    private MessageSourceException(string msg)
        : base(msg) { }

    public static MessageSourceException MessageSourceNotFound()
    {
        return new MessageSourceException($"Message source not found");
    }
}