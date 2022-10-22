using Isu.Extra.DomainExceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class MegaFaculty
{
    private readonly List<ExtraStudy> _studies;
    public MegaFaculty(GroupName groupName)
    {
        switch (groupName.Name[0])
        {
            case 'M':
            case 'K':
            case 'J':
                Name = "TINT";
                break;
            case 'R':
            case 'P':
            case 'N':
            case 'H':
                Name = "CTU";
                break;
            case 'G':
            case 'T':
            case 'O':
                Name = "BTINS";
                break;
            case 'U':
                Name = "FTMI";
                break;
            case 'L':
            case 'Z':
            case 'V':
                Name = "FT";
                break;
            default:
                throw MegaFacultyException.InvalidGroupName(groupName);
        }

        _studies = new List<ExtraStudy>();
    }

    public IReadOnlyList<ExtraStudy> Studies => _studies;
    public string Name { get; }

    public ExtraStudy CreateExtraStudy(string name)
    {
        var extraStudy = new ExtraStudy(name, this);
        _studies.Add(extraStudy);
        return extraStudy;
    }
}