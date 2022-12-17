using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using ApplicationLayer.Dto;
using ApplicationLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Messages;
using DataAccessLayer.Models.MessageSources;
using Microsoft.VisualBasic;

namespace ApplicationLayer.Services.Implementations;

public class MessageSourceService : IMessageSourceService
{
    private readonly DatabaseContext _context;

    public MessageSourceService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<MessageSourceDto> AddMessageSource(Guid accountId, string type, string name, Guid employeeId, CancellationToken token)
    {
        var employee = _context.Employees.OfType<Worker>().FirstOrDefault(x => x.Id == employeeId);
        if (employee == null)
            throw new NullReferenceException();
        var accounts = _context.Accounts.Where(a => a.AccessLevel.LevelValue >= employee.AccessLevel.LevelValue);
        if (accounts == null)
            throw new NullReferenceException();
        
        var messages = new Collection<BaseMessage>();
        MessageSource? msgSource = type switch
        {
            "email" => new EmailMessageSource(name, messages),
            "sms" => new SmsMessageSource(name, messages),
            "mobile" => new MobileMessageSource(name, messages),
            _ => null
        };
        if (msgSource == null)
            throw new NullReferenceException();
        var account = accounts.FirstOrDefault(a => a.Id == accountId);
        account?.Sources.Add(msgSource);
        await _context.SaveChangesAsync(token);
        return msgSource.AsDto();
    }
}