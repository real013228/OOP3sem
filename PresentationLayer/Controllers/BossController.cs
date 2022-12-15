using ApplicationLayer.Dto;
using ApplicationLayer.Services;
using DataAccessLayer.Models.Levels;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BossController : ControllerBase
{
    private readonly ICreateEmployee _service;

    public BossController(ICreateEmployee service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<ManagerDto>> CreateBossAsync([FromBody] CreateBoss model)
    {
        var boss = await _service.CreateBossAsync(model.Name, model.Password, CancellationToken);
        return Ok(boss);
    }
}