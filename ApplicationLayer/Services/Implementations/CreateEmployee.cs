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
using Activity = DataAccessLayer.Models.Activity;

namespace ApplicationLayer.Services.Implementations;

public class CreateEmployee : ICreateEmployee
{
    private readonly DatabaseContext _context;

    public CreateEmployee(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ManagerDto> CreateBossAsync(string name, string password, CancellationToken token)
    {
        var bossId = Guid.NewGuid();
        var employees = new Collection<Employee>();
        var boss = new Manager(employees, name, password,bossId);
        _context.Employees.Add(boss);
        await _context.SaveChangesAsync(token);
        return boss.AsDto();
    }

    public async Task<ManagerDto> CreateManagerAsync(Guid session, string name, string password, CancellationToken token)
    {
        var gaySession = _context.Sessions.ToList().FirstOrDefault(x => x.Id == session);
        if (gaySession == null)
        {
            throw new NullReferenceException();
        }
        
        // Подумать над инвариантом 
        var employees = new Collection<Employee>();
        var parentManager = _context.Employees.OfType<Manager>().FirstOrDefault(x => x.Id == gaySession.EmployeeId);
        var manager = new Manager(employees, name, password, Guid.NewGuid());
        parentManager?.Employees.Add(manager);
        _context.Employees.Add(manager);
        await _context.SaveChangesAsync(token);
        return manager.AsDto();
    }

    public async Task<WorkerDto> CreateWorkerAsync(Guid session, string name, string password, Level accessLevel, CancellationToken token)
    {
        var gaySession = _context.Sessions.ToList().FirstOrDefault(x => x.Id == session);
        if (gaySession == null)
        {
            throw new NullReferenceException();
        }
        
        var parentManager = _context.Employees.OfType<Manager>().FirstOrDefault(x => x.Id == gaySession.EmployeeId);
        if (parentManager == null)
        {
            throw new NullReferenceException();
        }
        var messages = new Collection<BaseMessage>();
        var activity = new Activity(messages);
        var worker = new Worker(activity, accessLevel, name, password, session);
        parentManager?.Employees.Add(worker);
        _context.Employees.Add(worker);
        await _context.SaveChangesAsync(token);
        return worker.AsDto();
    }
}