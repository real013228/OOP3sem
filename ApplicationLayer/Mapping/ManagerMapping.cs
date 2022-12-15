using ApplicationLayer.Dto;
using DataAccessLayer.Models.Employees;

namespace ApplicationLayer.Mapping;

public static class ManagerMapping
{
    public static ManagerDto AsDto(this Manager manager)
        => new ManagerDto(manager.Employees, manager.Name, manager.Id);
}