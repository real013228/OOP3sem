namespace DataAccessLayer.Models.Messages;

public abstract class BaseMessage
{
    public BaseMessage(string messageValue, DateTime time, MessageStatus status, Guid id)
    {
        Id = id;
        MessageValue = messageValue;
        Time = time;
        Status = status;
    }

    protected BaseMessage()
    {
        MessageValue = null!;
    }

    public MessageStatus Status { get; set; }
    public Guid Id { get; set; }
    public string MessageValue { get; set; }
    public DateTime Time { get; set; }
}