using DataAccessLayer.Models.Messages;

namespace DataAccessLayer.Models;

public class Report
{
    public Report(ICollection<BaseMessage> messages)
    {
        Messages = messages;
    }

    protected Report()
    {
        Messages = null!;
    }

    public virtual ICollection<BaseMessage> Messages { get; set; }
}