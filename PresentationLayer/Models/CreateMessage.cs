namespace PresentationLayer.Models;

public record CreateMessage(string Type, string TextValue, string Theme, Guid Receiver);