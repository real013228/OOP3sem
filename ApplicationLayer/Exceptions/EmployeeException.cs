namespace ApplicationLayer.Exceptions;

public class EmployeeException : Exception
{
    private EmployeeException(string msg)
        : base(msg) { }

    public static EmployeeException EmployeeNotFoundException()
    {
        return new EmployeeException($"Employee: not found exception");
    }
}