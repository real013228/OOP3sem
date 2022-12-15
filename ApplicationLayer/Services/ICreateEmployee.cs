using ApplicationLayer.Dto;
using DataAccessLayer.Models.Levels;

namespace ApplicationLayer.Services;

public interface ICreateEmployee
{
    Task<ManagerDto> CreateBossAsync(string name, CancellationToken token);
    Task<ManagerDto> CreateManagerAsync(string name, Level accessLevel, CancellationToken token);
    Task<WorkerDto> CreateWorkerAsync(string name, Level accessLevel, CancellationToken token);
}