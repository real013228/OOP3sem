using System.Collections.ObjectModel;
using ApplicationLayer.Dto;
using ApplicationLayer.Factories;
using ApplicationLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Messages;
using DataAccessLayer.Models.MessageSources;

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
        Worker? employee = _context.Employees.OfType<Worker>().FirstOrDefault(x => x.Id == employeeId);
        if (employee == null)
            throw new ArgumentNullException();
        IQueryable<Account>? accounts = _context.Accounts.Where(a => a.AccessLevel.LevelValue >= employee.AccessLevel.LevelValue);
        if (accounts == null)
            throw new NullReferenceException();
        
        var messages = new Collection<BaseMessage>();
        MessageSource msgSource = SetMessageSource(type, name, messages);
        Account? account = accounts.FirstOrDefault(a => a.Id == accountId);
        account?.Sources.Add(msgSource);
        await _context.SaveChangesAsync(token);
        return msgSource.AsDto();
    }

    private MessageSource SetMessageSource(string type, string name, Collection<BaseMessage> messages)
    {
        MessageSource? msgSource = type switch
        {
            "email" => new EmailMessageSourceFactory().CreateMessageSource(name, messages),
            "sms" => new SmsMessageSourceFactory().CreateMessageSource(name, messages),
            "mobile" => new MobileMessageSourceFactory().CreateMessageSource(name, messages),
            _ => null
        };
        if (msgSource == null)
            throw new ArgumentNullException();
        return msgSource;
    }
}