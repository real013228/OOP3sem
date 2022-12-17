using DataAccessLayer.Models;
using DataAccessLayer.Models.Levels;

namespace PresentationLayer.Models;

public record CreateWorker(Guid Session, string Name, string Password, int Level);