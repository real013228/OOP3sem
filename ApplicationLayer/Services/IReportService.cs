using ApplicationLayer.Dto;

namespace ApplicationLayer.Services;

public interface IReportService
{
    Task<ReportDto> GetReport(Guid employeeId, Guid sessionId, CancellationToken token);
}