using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Factories;

public abstract class MessageFactory
{
    public abstract BaseMessage CreateMessage(string receiver, string text, string theme);
}