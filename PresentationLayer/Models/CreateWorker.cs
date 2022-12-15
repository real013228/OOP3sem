using DataAccessLayer.Models;
using DataAccessLayer.Models.Levels;

namespace PresentationLayer.Models;

public record CreateWorker(string Name, int Level);