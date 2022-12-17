using System.Collections.ObjectModel;
using ApplicationLayer.Dto;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Levels;
using DataAccessLayer.Models.Messages;
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
        if (_context.Employees.OfType<Manager>().Any())
        {
            throw EmployeeException.EmployeeNotFoundException();
        }
        var employees = new Collection<Employee>();
        var boss = new Manager(employees, name, password, Guid.NewGuid(), new Report(new List<BaseMessage>(), Guid.NewGuid()));
        _context.Employees.Add(boss);
        await _context.SaveChangesAsync(token);
        return boss.AsDto();
    }

    public async Task<ManagerDto> CreateManagerAsync(Guid session, string name, string password, CancellationToken token)
    {
        Session? secondSession = _context.Sessions.ToList().FirstOrDefault(x => x.Id == session);
        if (secondSession == null)
        {
            throw SessionException.SessionNotFound(session);
        }
        
        var employees = new Collection<Employee>();
        Manager? parentManager = _context.Employees.OfType<Manager>().FirstOrDefault(x => x.Id == secondSession.EmployeeId);
        var manager = new Manager(employees, name, password, Guid.NewGuid(), new Report(new List<BaseMessage>(), Guid.NewGuid()));
        parentManager?.Employees.Add(manager);
        _context.Employees.Add(manager);
        await _context.SaveChangesAsync(token);
        return manager.AsDto();
    }

    public async Task<WorkerDto> CreateWorkerAsync(Guid session, string name, string password, Level accessLevel, CancellationToken token)
    {
        Session? secondSession = _context.Sessions.FirstOrDefault(x => x.Id == session);
        if (secondSession == null)
        {
            throw SessionException.SessionNotFound(session);
        }
        
        Manager? parentManager = _context.Employees.OfType<Manager>().FirstOrDefault(x => x.Id == secondSession.EmployeeId);
        if (parentManager == null)
        {
            throw EmployeeException.EmployeeNotFoundException();
        }
        var sources = new Collection<BaseMessage>();
        var activity = new Activity(sources);
        var worker = new Worker(activity, accessLevel, name, password, session);
        parentManager.Employees.Add(worker);
        _context.Employees.Add(worker);
        await _context.SaveChangesAsync(token);
        return worker.AsDto();
    }
}