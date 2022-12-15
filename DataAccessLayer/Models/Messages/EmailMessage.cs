using System.Globalization;

namespace DataAccessLayer.Models.Messages;

public class EmailMessage : BaseMessage
{
    public EmailMessage(string theme, string sender, string messageValue, DateTime time, MessageStatus status, Guid id)
        : base(messageValue, time, status, id)
    {
        Theme = theme;
        Sender = sender;
    }

    protected EmailMessage()
    {
        Theme = null!;
        Sender = null!;
    }

    public string Theme { get; set; }
    public string Sender { get; set; }
}