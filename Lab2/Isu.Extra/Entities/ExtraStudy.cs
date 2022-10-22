using Isu.Extra.DomainExceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class ExtraStudy
{
    private readonly List<Stream> _streams;

    public ExtraStudy(string extraStudyName, MegaFaculty megaFaculty)
    {
        ExtraStudyName = extraStudyName;
        MegaFaculty = megaFaculty;
        _streams = new List<Stream>();
        Id = Guid.NewGuid();
    }

    public IReadOnlyList<Stream> Streams => _streams;
    public string ExtraStudyName { get; }
    public MegaFaculty MegaFaculty { get; }
    public Guid Id { get; }

    public Stream AddStream()
    {
        var stream = new Stream(Schedule.Builder.Build(), this);
        _streams.Add(stream);
        return stream;
    }

    public Stream GetStream()
    {
        Stream? stream = _streams.FirstOrDefault(x => x.StreamFull());
        return stream ?? throw ExtraStudyException.InvalidRequestException();
    }
}