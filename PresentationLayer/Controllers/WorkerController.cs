using ApplicationLayer.Dto;
using ApplicationLayer.Services;
using DataAccessLayer.Models.Levels;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController : ControllerBase
{
    private readonly ICreateEmployee _service;

    public WorkerController(ICreateEmployee service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<WorkerDto>> CreateWorkerAsync([FromBody] CreateWorker model)
    {
        var accessLevel = new Level(model.Level);
        var worker = await _service.CreateWorkerAsync(model.Session, model.Name, model.Password, accessLevel, CancellationToken);
        return Ok(worker);
    }
}