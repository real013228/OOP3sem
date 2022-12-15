using DataAccessLayer.Models.Levels;

namespace DataAccessLayer.Models.Employees;

public class Worker : Employee
{
    public Worker(Activity workerActivity, Level accessLevel, string name, Guid id)
        : base(name, id)
    {
        WorkerActivity = workerActivity;
        AccessLevel = accessLevel;
    }

    protected Worker()
    {
        WorkerActivity = null!;
    }

    public virtual Activity WorkerActivity { get; set; }
    public virtual Level AccessLevel { get; set; }
}