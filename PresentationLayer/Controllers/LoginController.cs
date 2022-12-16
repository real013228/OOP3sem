using ApplicationLayer.Dto;
using ApplicationLayer.Services;
using DataAccessLayer.Models.Levels;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IAuthoriseService _service;

    public LoginController(IAuthoriseService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<SessionDto>> LoginAsync([FromBody] LoginModel model)
    {
        var session = await _service.LoginAsync(model.Name, model.Password, CancellationToken);
        return Ok(session);
    }
}