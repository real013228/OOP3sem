using ApplicationLayer.Dto;
using ApplicationLayer.Services;
using DataAccessLayer.Models.Levels;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreatingMessageController : ControllerBase
{
    private readonly IMessagesService _service;

    public CreatingMessageController(IMessagesService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessageAsync([FromBody] CreateMessage model)
    {
        var msg = await _service.CreateMessage(model.Receiver, model.Type, model.TextValue, model.Theme, CancellationToken);
        return Ok(msg);
    }
}