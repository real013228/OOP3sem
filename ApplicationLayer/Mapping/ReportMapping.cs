using System.Collections.ObjectModel;
using ApplicationLayer.Dto;
using DataAccessLayer.Models;
using Microsoft.VisualBasic;

namespace ApplicationLayer.Mapping;

public static class ReportMapping
{
    public static ReportDto AsDto(this Report report)
    {
        var messages = new Collection<MessageDto>();
        foreach (var msg in report.Messages)
        {
            messages.Add(msg.AsDto());
        }
        return new ReportDto(messages, report.Messages.Count);   
    }
}