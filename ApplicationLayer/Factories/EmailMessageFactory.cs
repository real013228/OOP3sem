using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Factories;

public class EmailMessageFactory : MessageFactory
{
    public override BaseMessage CreateMessage(string receiver, string text, string theme)
    {
        return new EmailMessage(theme, text, DateTime.Now, MessageStatus.New, Guid.NewGuid());
    }
}