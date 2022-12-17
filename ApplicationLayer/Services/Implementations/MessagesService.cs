using ApplicationLayer.Dto;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Factories;
using ApplicationLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Messages;
using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Services.Implementations;

public class MessagesService : IMessagesService
{
    private readonly DatabaseContext _context;

    public MessagesService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<MessageDto>> GetMessagesAsync(Guid employeeId, Guid sessionId, CancellationToken token)
    {
        if (!_context.Sessions.Any(x => x.Id == sessionId))
            throw SessionException.SessionNotFound(sessionId);
        
        Worker? employee = _context.Employees.OfType<Worker>().FirstOrDefault(e => e.Id == employeeId);
        if (employee == null)
            throw EmployeeException.EmployeeNotFoundException();
        
        IQueryable<Account> accounts = _context.Accounts.Where(a => a.AccessLevel.LevelValue >= employee.AccessLevel.LevelValue);
        if (!accounts.Any())
            throw AccountException.AccountNotFound();
        
        IQueryable<MessageSource> sources = accounts.SelectMany(acc => acc.Sources);

        if (!sources.Any())
            throw MessageSourceException.MessageSourceNotFound();
        var messagesDto = new List<MessageDto>();
        foreach (BaseMessage? msg in sources.SelectMany(src => src.Messages))
        {
            messagesDto.Add(msg.AsDto());
            BaseMessage? baseMessage = _context.Messages.FirstOrDefault(m => m.Id == msg.Id);
            if (baseMessage == null)
                throw MessageException.MessageNotFound();
            if (baseMessage.Status == MessageStatus.New)
                baseMessage.Status = MessageStatus.Received;
        }

        if (!messagesDto.Any())
            throw MessageException.MessageNotFound();
        await _context.SaveChangesAsync(token);
        return messagesDto;
    }

    public async Task<MessageDto> CreateMessage(Guid sourceId, string type, string text, string theme, CancellationToken token)
    {
        MessageSource? source = null;
        MessageFactory? factory = null;
        GetMessageFactory(type, sourceId, ref factory, ref source);
        BaseMessage msg = factory!.CreateMessage(source!.Login, text, theme);
        _context.Messages.Add(msg);
        source.Messages.Add(msg);
        await _context.SaveChangesAsync(token);
        return msg.AsDto();
    }

    private void GetMessageFactory(string type, Guid sourceId, ref MessageFactory? factory, ref MessageSource? source)
    {
        switch (type)
        {
            case "email":
                factory = new EmailMessageFactory();
                source = _context.MessageSources.OfType<EmailMessageSource>().FirstOrDefault(s => s.Id == sourceId);
                break;
            case "sms":
                factory = new SmsMessageFactory();
                source = _context.MessageSources.OfType<SmsMessageSource>().FirstOrDefault(s => s.Id == sourceId);
                break;
            case "mobile":
                factory = new MobileMessageFactory();
                source = _context.MessageSources.OfType<MobileMessageSource>().FirstOrDefault(s => s.Id == sourceId);
                break;
        }

        if (factory == null || source == null)
            throw MessageSourceException.MessageSourceNotFound();
    }
}