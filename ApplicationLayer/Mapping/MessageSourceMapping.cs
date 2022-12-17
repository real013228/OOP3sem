using System.Collections.ObjectModel;
using ApplicationLayer.Dto;
using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Mapping;

public static class MessageSourceMapping
{
    public static MessageSourceDto AsDto(this MessageSource model)
    {
        var messages = model.Messages.Select(x => x.AsDto()).ToList();
        return new MessageSourceDto(model.Login, messages, Guid.NewGuid());   
    }
}