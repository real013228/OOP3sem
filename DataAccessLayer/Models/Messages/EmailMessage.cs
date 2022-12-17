using System.Globalization;

namespace DataAccessLayer.Models.Messages;

public class EmailMessage : BaseMessage
{
    public EmailMessage(string theme, string messageValue, DateTime time, MessageStatus status, Guid id)
        : base(messageValue, time, status, id)
    {
        Theme = theme;
    }

    protected EmailMessage()
    {
        Theme = null!;
    }

    public string Theme { get; set; }
}