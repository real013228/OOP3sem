using ApplicationLayer.Dto;
using ApplicationLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.Implementations;

public class AuthoriseService : IAuthoriseService
{
    private readonly DatabaseContext _context;

    public AuthoriseService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<SessionDto> LoginAsync(string name, string password, CancellationToken token)
    {
        var employee = _context.Employees.FirstOrDefault(x => x.EmployeeName == name);
        if (employee == null || employee.EmployeePassword != password)
        {
            throw new NullReferenceException();
        }

        var session = new Session(Guid.NewGuid(), employee.Id);
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync(token);
        return session.AsDto();
    }

    public async Task LogoutAsync(Guid sessionId, CancellationToken token)
    {
        Session? session = _context.Sessions.FirstOrDefault(x => x.Id == sessionId);
        if (session != null)
            _context.Sessions.Remove(session);
        await _context.SaveChangesAsync(token);
    }
}