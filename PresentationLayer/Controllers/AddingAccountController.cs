using ApplicationLayer.Dto;
using ApplicationLayer.Services;
using DataAccessLayer.Models.Levels;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddingAccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AddingAccountController(IAccountService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<AccountDto>> AddAccountAsync([FromBody] CreateAccount model)
    {
        var account = await _service.AddAccount(model.Name, model.Level, CancellationToken);
        return Ok(account);
    }
}