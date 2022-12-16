﻿using ApplicationLayer.Dto;
using ApplicationLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Services.Implementations;

public class HandleMessage : IHandleMessage
{
    private readonly DatabaseContext _context;

    public HandleMessage(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<MessageDto> Handle(Guid sessionId, Guid messageId, CancellationToken token)
    {
        Session? session = _context.Sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session == null)
            throw new NullReferenceException();
        BaseMessage? message = _context.Messages.FirstOrDefault(m => m.Id == messageId);
        if (message == null || message.Status == MessageStatus.Handled)
            throw new NullReferenceException();
        message.Status = MessageStatus.Handled;
        Worker? employee = _context.Employees.OfType<Worker>().FirstOrDefault(x => x.Id == session.EmployeeId);
        if (employee == null)
            throw new NullReferenceException();
        employee.WorkerActivity.Messages.Add(message);
        await _context.SaveChangesAsync(token);
        return message.AsDto();
    }
}