namespace Isu.Services;

[Serializable]
public class CreateGroupWithInvalidNameException : Exception
{
    public CreateGroupWithInvalidNameException() { }

    public CreateGroupWithInvalidNameException(string message)
        : base(message)
    { }

    public CreateGroupWithInvalidNameException(string message, Exception inner)
        : base(message, inner)
    { }
}