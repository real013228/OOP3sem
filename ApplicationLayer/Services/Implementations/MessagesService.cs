using ApplicationLayer.Dto;
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
        {
            throw new NullReferenceException();
        }
        Worker? employee = _context.Employees.OfType<Worker>().FirstOrDefault(e => e.Id == employeeId);
        if (employee == null)
            throw new NullReferenceException();
        IQueryable<Account> accounts = _context.Accounts.Where(a => a.AccessLevel.LevelValue >= employee.AccessLevel.LevelValue);
        if (!accounts.Any())
            throw new NullReferenceException();
        var sources = new List<MessageSource>();
        foreach (Account acc in accounts)
        {
            sources.AddRange(acc.Sources);
        }

        if (!sources.Any())
            throw new NullReferenceException();
        var messagesDto = new List<MessageDto>();
        foreach (BaseMessage? msg in sources.SelectMany(src => src.Messages))
        {
            messagesDto.Add(msg.AsDto());
            BaseMessage? baseMessage = _context.Messages.FirstOrDefault(m => m.Id == msg.Id);
            if (baseMessage == null)
                throw new NullReferenceException();
            if (baseMessage.Status == MessageStatus.New)
                baseMessage.Status = MessageStatus.Received;
        }

        if (!messagesDto.Any())
            throw new NullReferenceException();
        await _context.SaveChangesAsync(token);
        return messagesDto;
    }

    public async Task<MessageDto> CreateMessage(Guid sourceId, string type, string text, string theme, CancellationToken token)
    {
        MessageSource? source = null;
        MessageFactory? factory = null;
        GetMessageFactory(type, sourceId, factory, source);
        BaseMessage msg = factory!.CreateMessage(source!.Login, text, theme);
        _context.Messages.Add(msg);
        source.Messages.Add(msg);
        await _context.SaveChangesAsync(token);
        return msg.AsDto();
    }

    private void GetMessageFactory(string type, Guid sourceId, MessageFactory? factory, MessageSource? source)
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
            throw new ArgumentNullException();
    }
}