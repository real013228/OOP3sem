using ApplicationLayer.Dto;
using DataAccessLayer.Models.Messages;
using DataAccessLayer.Models.MessageSources;

namespace ApplicationLayer.Mapping;

public static class MessageSourceMapping
{
    public static MessageSourceDto AsDto(this MessageSource model)
    {
        return new MessageSourceDto(model.Login, model.Messages, Guid.NewGuid());   
    }
}