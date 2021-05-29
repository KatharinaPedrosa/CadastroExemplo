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
    public class ClientController : Controller
    {
        private IClientService service;

        public ClientController(IClientService service)
        {
            this.service = service;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] Client dto)
        {
            try
            {
                var result = await service.AddClient(dto);
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
                var result = await service.DeleteClient(id);
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
                var result = await service.GetClients();
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
                var result = await service.GetClient(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] Client dto)
        {
            try
            {
                var result = await service.UpdateClient(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}