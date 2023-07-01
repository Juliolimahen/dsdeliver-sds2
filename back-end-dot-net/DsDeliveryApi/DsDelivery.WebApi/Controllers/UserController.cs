using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using DsDelivery.Manager.Interfaces;
using DsDelivery.Manager.Services;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.User;

namespace DsDelivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService _service)
        {
            this._service = _service;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var userLogged = await _service.ValidaUserEGeraTokenAsync(user);
            if (userLogged != null)
            {
                return Ok(userLogged);
            }
            return Unauthorized();
        }

        [Authorize(Roles = "Presidente, Lider, Diretor")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string login = User.Identity.Name;
            var user = await _service.GetAsync(login);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserDTO user)
        {
            var userInserido = await _service.InsertAsync(user);
            return CreatedAtAction(nameof(Get), new { login = user.Login }, userInserido);
        }
    }
}
