namespace Isu.Extra.DomainExceptions;

public class IsuExtraServiceExceptions : Exception
{
    private IsuExtraServiceExceptions(string msg)
        : base(msg)
    {
    }

    public static IsuExtraServiceExceptions InvalidStudentId(int id)
    {
        return new IsuExtraServiceExceptions($"Invalid ID:{id.ToString()} There is not student with same ID");
    }
}