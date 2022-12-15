using System.Collections.ObjectModel;
using System.Diagnostics;
using ApplicationLayer.Dto;
using ApplicationLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.Models;
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
        var employees = new Collection<Employee>();
        var boss = new Manager(employees, name, bossId);
        _context.Employees.Add(boss);
        await _context.SaveChangesAsync(token);
        return boss.AsDto();
    }

    public async Task<ManagerDto> CreateManagerAsync(Guid session, string name, CancellationToken token)
    {
        var gaySession = _context.Sessions.ToList().FirstOrDefault(x => x.Id == session);
        if (gaySession == null)
        {
            throw new NullReferenceException();
        }
        
        // Подумать над инвариантом 
        var employees = new Collection<Employee>();
        var parentManager = _context.Employees.OfType<Manager>().FirstOrDefault(x => x.Id == gaySession.Id);
        var manager = new Manager(employees, name, Guid.NewGuid());
        _context.Employees.Add(manager);
    }

    public async Task<WorkerDto> CreateWorkerAsync(Guid session, string name, Level accessLevel, CancellationToken token)
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