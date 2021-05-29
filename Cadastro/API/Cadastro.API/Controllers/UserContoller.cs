using System;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.Services;
using Cadastro.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("/[controller]")]
    public class UserContoller : Controller
    {
        private IUserService service;

        public UserContoller(IUserService service)
        {
            this.service = service;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] User dto)
        {
            try
            {
                var result = await service.AddUser(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await service.DeleteUser(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            try
            {
                var result = await service.GetUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var result = await service.GetUser(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] User dto)
        {
            try
            {
                var result = await service.UpdateUser(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}