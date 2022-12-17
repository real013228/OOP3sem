using DataAccessLayer.Models.Messages;
using DataAccessLayer.Models.MessageSources;

namespace DataAccessLayer.Models;

public class Activity
{
    public Activity(ICollection<BaseMessage> messages)
    {
        Messages = messages;
        Id = Guid.NewGuid();
    }

    protected Activity()
    {
        Messages = null!;
    }

    public virtual ICollection<BaseMessage> Messages { get; set; }
    public Guid Id { get; set; }
}