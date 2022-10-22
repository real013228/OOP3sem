using Isu.Extra.Entities;
using Isu.Models;

namespace Isu.Extra.Models;

public class GroupNameExtra
{
    public GroupNameExtra(GroupName groupName)
    {
        GroupName = groupName;
        MegaFaculty = new MegaFaculty(groupName);
    }

    public GroupName GroupName { get; }
    public MegaFaculty MegaFaculty { get; }
}