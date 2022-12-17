using System.Collections.ObjectModel;
using ApplicationLayer.Dto;
using DataAccessLayer.Models;

namespace ApplicationLayer.Mapping;

public static class ReportMapping
{
    public static ReportDto AsDto(this Report report)
    {
        var messages = report.Messages.Select(x => x.AsDto()).ToList();
        return new ReportDto(messages, report.Messages.Count);   
    }
}