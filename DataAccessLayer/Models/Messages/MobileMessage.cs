namespace DataAccessLayer.Models.Messages;

public class MobileMessage : BaseMessage
{
    public MobileMessage(string number, string messageValue, DateTime time, MessageStatus status, Guid id)
        : base(messageValue, time, status, id)
    {
        Number = number;
    }

    protected MobileMessage()
    {
        Number = null!;
    }

    public string Number { get; }
}