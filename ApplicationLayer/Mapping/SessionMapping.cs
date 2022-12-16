using ApplicationLayer.Dto;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Employees;

namespace ApplicationLayer.Mapping;

public static class SessionMapping
{
    public static SessionDto AsDto(this Session session)
        => new SessionDto(session.Id, session.EmployeeId);
}