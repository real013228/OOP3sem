using DataAccessLayer.Models.Messages;

namespace DataAccessLayer.Models.MessageSources;

public class SmsMessageSource : MessageSource
{
    public SmsMessageSource(string login, ICollection<BaseMessage> messages)
        : base(login, messages) { }
    protected SmsMessageSource() { }
}