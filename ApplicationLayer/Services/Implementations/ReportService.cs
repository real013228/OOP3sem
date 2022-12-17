using ApplicationLayer.Dto;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Services.Implementations;

public class ReportService : IReportService
{
    private readonly DatabaseContext _context;

    public ReportService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ReportDto> GetReport(Guid employeeId, Guid sessionId, CancellationToken token)
    {
        if (!_context.Sessions.Any(x => x.Id == sessionId))
            throw SessionException.SessionNotFound(sessionId);

        Manager? employee = _context.Employees.OfType<Manager>().FirstOrDefault(e => e.Id == employeeId);
        if (employee == null)
            throw EmployeeException.EmployeeNotFoundException();

        if (!employee.Employees.Any())
            throw EmployeeException.EmployeeNotFoundException();
        foreach (Employee? emp in employee.Employees)
        {
            if (emp is not Worker worker) continue;
            if (worker.WorkerActivity.Messages == null)
                throw EmployeeException.EmployeeNotFoundException();
            foreach (BaseMessage? msg in worker.WorkerActivity.Messages)
            {
                if (msg.Status == MessageStatus.Handled)
                {
                    employee.Report.Messages.Add(msg);
                }
            }
        }

        await _context.SaveChangesAsync(token);
        return employee.Report.AsDto();
    }
}