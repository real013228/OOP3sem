using ApplicationLayer.Dto;
using ApplicationLayer.Services;
using DataAccessLayer.Models.Levels;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HandlingMessage : ControllerBase
{
    private readonly IHandleMessage _service;

    public HandlingMessage(IHandleMessage service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<MessageDto>> HandleMessage([FromBody] HandleMsg model)
    {
        var msg = await _service.Handle(model.SessionId, model.MessageId, CancellationToken);
        return Ok(msg);
    }
}