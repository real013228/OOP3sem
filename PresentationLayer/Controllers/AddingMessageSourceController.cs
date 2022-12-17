using ApplicationLayer.Dto;
using ApplicationLayer.Services;
using DataAccessLayer.Models.Levels;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddingMessageSourceController : ControllerBase
{
    private readonly IMessageSourceService _service;

    public AddingMessageSourceController(IMessageSourceService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<MessageSourceDto>> AddMessageSourceAsync([FromBody] CreateMessageSource model)
    {
        var msgSource = await _service.AddMessageSource(model.AccountId, model.Type, model.Name, model.EmployeeId, CancellationToken);
        return Ok(msgSource);
    }
}