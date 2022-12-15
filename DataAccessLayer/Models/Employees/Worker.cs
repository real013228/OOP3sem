using DataAccessLayer.Models.Levels;

namespace DataAccessLayer.Models.Employees;

public class Worker : Employee
{
    public Worker(Activity workerActivity, Level accessLevel, string name, Guid id)
        : base(name, accessLevel, id)
    {
        WorkerActivity = workerActivity;
    }

    protected Worker()
    {
        WorkerActivity = null!;
    }

    public virtual Activity WorkerActivity { get; set; }
}