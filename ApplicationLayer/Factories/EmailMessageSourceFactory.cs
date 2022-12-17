using DataAccessLayer.Models.Messages;
using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Factories;

public class EmailMessageSourceFactory : MessageSourceFactory
{
    public override MessageSource CreateMessageSource(string login, ICollection<BaseMessage> messages)
    {
        return new EmailMessageSource(login, messages);
    }
}