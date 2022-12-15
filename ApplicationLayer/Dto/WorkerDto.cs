using DataAccessLayer.Models;
using DataAccessLayer.Models.Levels;

namespace ApplicationLayer.Dto;

public record WorkerDto(Activity WorkerActivity, Level AccessLevel, string Name, string Password, Guid Id);