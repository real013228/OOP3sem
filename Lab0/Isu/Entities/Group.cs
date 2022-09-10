using Isu.Models;
using Isu.Services;

namespace Isu.Entities;

public class Group
{
    public Group(GroupName name)
    {
        NameOfGroup = name;
    }

    public GroupName NameOfGroup { get; }
}
