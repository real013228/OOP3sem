namespace ApplicationLayer.Exceptions;

public class SessionException : Exception
{
    private SessionException(string msg)
        : base(msg) { }

    public static SessionException SessionNotFound(Guid sessionId)
    {
        return new SessionException($"Session: {sessionId} not found");
    }
}