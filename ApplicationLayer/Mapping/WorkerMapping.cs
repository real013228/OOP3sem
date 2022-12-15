using ApplicationLayer.Dto;
using DataAccessLayer.Models.Employees;

namespace ApplicationLayer.Mapping;

public static class WorkerMapping
{
    public static WorkerDto AsDto(this Worker worker)
        => new WorkerDto(worker.WorkerActivity, worker.AccessLevel, worker.Name, worker.Id);
}