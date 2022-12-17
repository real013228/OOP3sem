using System.Collections.ObjectModel;
using ApplicationLayer.Dto;
using DataAccessLayer.Models.Messages;
using DataAccessLayer.Models.MessageSources;
using Microsoft.VisualBasic;

namespace ApplicationLayer.Mapping;

public static class MessageSourceMapping
{
    public static MessageSourceDto AsDto(this MessageSource model)
    {
        var messages = new Collection<MessageDto>();
        foreach (var msg in model.Messages)
        {
            messages.Add(msg.AsDto());
        }
        return new MessageSourceDto(model.Login, messages, Guid.NewGuid());   
    }
}