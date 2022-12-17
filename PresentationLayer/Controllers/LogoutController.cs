using ApplicationLayer.Dto;
using ApplicationLayer.Services;
using DataAccessLayer.Models.Levels;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogoutController : ControllerBase
{
    private readonly IAuthoriseService _service;

    public LogoutController(IAuthoriseService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult> LogoutAsync([FromBody] LogoutModel model)
    {
        await _service.LogoutAsync(model.sessionId, CancellationToken);
        return Ok();
    }
}