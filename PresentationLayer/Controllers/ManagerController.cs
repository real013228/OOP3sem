using ApplicationLayer.Dto;
using ApplicationLayer.Services;
using DataAccessLayer.Models.Levels;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManagerController : ControllerBase
{
    private readonly ICreateEmployee _service;

    public ManagerController(ICreateEmployee service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<ManagerDto>> CreateManagerAsync([FromBody] CreateManager model)
    {
        var accessLevel = new Level(model.Level);
        var worker = await _service.CreateWorkerAsync(model.Name, accessLevel, CancellationToken);
        return Ok(worker);
    }
}