using System.Collections.ObjectModel;
using System.Diagnostics;
using ApplicationLayer.Dto;
using ApplicationLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Levels;
using DataAccessLayer.Models.Messages;
using Microsoft.VisualBasic;

namespace ApplicationLayer.Services.Implementations;

public class CreateEmployee : ICreateEmployee
{
    private readonly DatabaseContext _context;

    public CreateEmployee(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ManagerDto> CreateBossAsync(string name, CancellationToken token)
    {
        var bossId = Guid.NewGuid();
        var bossLevel = new Level(0);
        var employees = new Collection<Employee>();
        var boss = new Manager(employees, bossLevel, name, bossId);
        _context.Employees.Add(boss);
        await _context.SaveChangesAsync(token);
        return boss.AsDto();
    }

    public async Task<ManagerDto> CreateManagerAsync(string name, Level accessLevel, CancellationToken token)
    {
        var managerId = Guid.NewGuid();
        var employees = new Collection<Employee>();
        var manager = new Manager(employees, accessLevel, name, managerId);
        _context.Employees.Add(manager);
        await _context.SaveChangesAsync(token);
        return manager.AsDto();
    }

    public async Task<WorkerDto> CreateWorkerAsync(string name, Level accessLevel, CancellationToken token)
    {
        var workerId = Guid.NewGuid();
        var messages = new Collection<BaseMessage>();
        var workerActivity = new DataAccessLayer.Models.Activity(messages);
        var worker = new Worker(workerActivity, accessLevel, name, workerId);
        _context.Employees.Add(worker);
        await _context.SaveChangesAsync(token);
        return worker.AsDto();
    }
}