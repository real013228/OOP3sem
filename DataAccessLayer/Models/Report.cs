using DataAccessLayer.Models.Messages;

namespace DataAccessLayer.Models;

public class Report
{
    public Report(ICollection<BaseMessage> messages, Guid id)
    {
        Messages = messages;
        Id = id;
    }

    protected Report()
    {
        Messages = null!;
    }

    public Guid Id { get; set; }
    public virtual ICollection<BaseMessage> Messages { get; set; }
}