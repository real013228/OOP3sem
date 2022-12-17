using ApplicationLayer.Dto;
using ApplicationLayer.Services;
using DataAccessLayer.Models.Levels;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GettingMessagesController : ControllerBase
{
    private readonly IMessagesService _service;

    public GettingMessagesController(IMessagesService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<IReadOnlyList<MessageDto>>> GetMessagesAsync([FromBody] GetMessages model)
    {
        IReadOnlyList<MessageDto> messages = await _service.GetMessagesAsync(model.EmployeeId, model.SessionId, CancellationToken);
        return Ok(messages);
    }
}