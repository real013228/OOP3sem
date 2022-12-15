using ApplicationLayer.Dto;
using DataAccessLayer.Models.Employees;

namespace ApplicationLayer.Mapping;

public static class ManagerMapping
{
    public static ManagerDto AsDto(this Manager manager)
        => new ManagerDto(manager.Employees, manager.AccessLevel, manager.Name, manager.Id);
}