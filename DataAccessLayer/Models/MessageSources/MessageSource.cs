using DataAccessLayer.Models.Messages;

namespace DataAccessLayer.Models.MessageSources;

public abstract class MessageSource
{
    protected MessageSource(string login, ICollection<BaseMessage> messages)
    {
        Login = login;
        Messages = messages;
        Id = Guid.NewGuid();
    }

    protected MessageSource()
    {
        Login = null!;
        Messages = null!;
    }

    public Guid Id { get; set; }
    public string Login { get; set; }
    public virtual ICollection<BaseMessage> Messages { get; set; }
}