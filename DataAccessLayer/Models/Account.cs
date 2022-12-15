using DataAccessLayer.Models.Levels;
using DataAccessLayer.Models.Messages;
using DataAccessLayer.Models.MessageSources;

namespace DataAccessLayer.Models;

public class Account
{
    public Account(ICollection<MessageSource> sources, Level accessLevel, Guid id)
    {
        Sources = sources;
        AccessLevel = accessLevel;
        Id = id;
    }

    protected Account()
    {
        Sources = null!;
        AccessLevel = null!;
    }

    public Guid Id { get; set; }
    public virtual Level AccessLevel { get; set; }
    public virtual ICollection<MessageSource> Sources { get; set; }
}