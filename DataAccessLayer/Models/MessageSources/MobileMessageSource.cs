using DataAccessLayer.Models.Messages;

namespace DataAccessLayer.Models.MessageSources;

public class MobileMessageSource : MessageSource
{
    public MobileMessageSource(string login, ICollection<BaseMessage> messages)
        : base(login, messages) { }
    protected MobileMessageSource() { }
}