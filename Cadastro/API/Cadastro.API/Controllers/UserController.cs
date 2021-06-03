using System;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.Services;
using Cadastro.Domain.DTOs;
using Cadastro.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("/[controller]")]
    public class UserController : BaseController<User, UserEntity>
    {
        private IUserService service;

        public UserController(IUserService service)
            : base(service)
        {
            this.service = service;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public virtual async Task<IActionResult> Login([FromBody] User dto)
        {
            try
            {
                var result = await service.Login(dto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}