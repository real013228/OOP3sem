using System.Collections.ObjectModel;
using DataAccessLayer.Models.Messages;
using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Factories;

public abstract class MessageSourceFactory
{
    public abstract MessageSource CreateMessageSource(string login, ICollection<BaseMessage> messages);
}