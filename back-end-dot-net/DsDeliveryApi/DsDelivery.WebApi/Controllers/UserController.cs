using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DsDelivery.Manager.Interfaces;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.User;

namespace DsDelivery.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var userLogado = await _service.ValidaUserEGeraTokenAsync(user);
        if (userLogado != null)
        {
            return Ok(userLogado);
        }
        return Unauthorized();
    }

    [Authorize(Roles = "Diretor")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        string login = User.Identity.Name;
        var user = await _service.GetAsync(login);
        return Ok(user);
    }

    [Authorize(Roles = "Diretor")]
    [HttpPost]
    public async Task<IActionResult> Post(CreateUserDTO user)
    {
        var userInserido = await _service.InsertAsync(user);
        return CreatedAtAction(nameof(Get), new { login = user.Login }, userInserido);
    }
}
