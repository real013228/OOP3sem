using ApplicationLayer.Dto;
using DataAccessLayer.Models;

namespace ApplicationLayer.Mapping;

public static class ReportMapping
{
    public static ReportDto AsDto(this Report report)
        => new ReportDto(report.Messages, report.Messages.Count);
}