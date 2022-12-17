using ApplicationLayer.Dto;
using DataAccessLayer.Models.Levels;

namespace ApplicationLayer.Services;

public interface ICreateEmployee
{
    Task<ManagerDto> CreateBossAsync(string name, string password, CancellationToken token);
    Task<ManagerDto> CreateManagerAsync(Guid session, string name, string password, CancellationToken token);
    Task<WorkerDto> CreateWorkerAsync(Guid session, string name, string password, Level accessLevel, CancellationToken token);
}