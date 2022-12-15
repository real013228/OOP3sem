using ApplicationLayer.Dto;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Levels;

namespace ApplicationLayer.Services;

public interface ICreateEmployee
{
    Task<ManagerDto> CreateBossAsync(string name, CancellationToken token);
    Task<ManagerDto> CreateManagerAsync(Guid session, string name, CancellationToken token);
    Task<WorkerDto> CreateWorkerAsync(Guid session, string name, Level accessLevel, CancellationToken token);
}