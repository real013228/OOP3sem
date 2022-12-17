using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Factories;

public class MobileMessageFactory : MessageFactory
{
    public override BaseMessage CreateMessage(string receiver, string text, string theme)
    {
        return new MobileMessage(theme, text, DateTime.Now, MessageStatus.New, Guid.NewGuid());
    }
}