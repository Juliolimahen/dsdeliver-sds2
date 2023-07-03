using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using DsDelivery.Manager.Interfaces;
using DsDelivery.Manager.Services;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.User;

namespace DsDelivery.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IUserService manager;

    public UserController(IUserService manager)
    {
        this.manager = manager;
    }

    [HttpGet]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var userLogado = await manager.ValidaUserEGeraTokenAsync(user);
        if (userLogado != null)
        {
            return Ok(userLogado);
        }
        return Unauthorized();
    }

    //[Authorize(Roles = "Presidente, Lider")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        string login = User.Identity.Name;
        var user = await manager.GetAsync(login);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateUserDTO user)
    {
        var userInserido = await manager.InsertAsync(user);
        return CreatedAtAction(nameof(Get), new { login = user.Login }, userInserido);
    }
}
