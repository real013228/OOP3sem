namespace DataAccessLayer.Models;

public class Session
{
    public Session(Guid id, Guid employeeId)
    {
        EmployeeId = employeeId;
        Id = id;
    }

    protected Session()
    {
    }

    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
}