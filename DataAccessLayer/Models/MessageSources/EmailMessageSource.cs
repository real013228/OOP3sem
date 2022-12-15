using DataAccessLayer.Models.Messages;

namespace DataAccessLayer.Models.MessageSources;

public class EmailMessageSource : MessageSource
{
    public EmailMessageSource(string login, ICollection<BaseMessage> messages)
        : base(login, messages) { }

    protected EmailMessageSource() { }
}