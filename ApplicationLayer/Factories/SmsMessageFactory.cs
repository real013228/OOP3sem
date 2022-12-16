using DataAccessLayer;
using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Factories;

public class SmsMessageFactory : MessageFactory
{
    public override BaseMessage CreateMessage(string receiver, string text, string theme)
    {
        return new SmsMessage(text, theme, DateTime.Now, MessageStatus.New, Guid.NewGuid());
    }
}