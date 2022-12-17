using DataAccessLayer.Models.Messages;
using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Factories;

public class MobileMessageSourceFactory : MessageSourceFactory
{
    public override MessageSource CreateMessageSource(string login, ICollection<BaseMessage> messages)
    {
        return new MobileMessageSource(login, messages);
    }
}