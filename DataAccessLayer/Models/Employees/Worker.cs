using DataAccessLayer.Models.Levels;

namespace DataAccessLayer.Models.Employees;

public class Worker : Employee
{
    public Worker(Activity workerActivity, Level accessLevel, string name, string password, Guid workerId)
        : base(name, password, workerId)
    {
        WorkerActivity = workerActivity;
        AccessLevel = accessLevel;
    }

    protected Worker()
    {
        WorkerActivity = null!;
        AccessLevel = null!;
    }

    public virtual Activity WorkerActivity { get; set; }
    public virtual Level AccessLevel { get; set; }
}