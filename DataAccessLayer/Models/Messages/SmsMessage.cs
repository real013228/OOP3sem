namespace DataAccessLayer.Models.Messages;

public class SmsMessage : BaseMessage
{
    public SmsMessage(string messageValue, string theme, DateTime time, MessageStatus status, Guid id)
        : base(messageValue, time, status, id)
    {
        Theme = theme;
    }

    protected SmsMessage()
    {
        Theme = null!;
    }

    public string Theme { get; set; }
}