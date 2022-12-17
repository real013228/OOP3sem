using ApplicationLayer.Dto;
using DataAccessLayer.Models.Messages;

namespace ApplicationLayer.Mapping;

public static class MessageMapping
{
    public static MessageDto AsDto(this BaseMessage model)
    {
        return new MessageDto(model.MessageValue, model.Time, model.Status, model.Id);   
    }
}