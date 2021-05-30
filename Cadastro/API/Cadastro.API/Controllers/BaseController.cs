using System;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.DTOs;
using Cadastro.Domain.Abstraction.Entities;
using Cadastro.Domain.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("/[controller]")]
    public class BaseController<TDTO, TEntity> : Controller
        where TDTO : class, IDTO, new()
        where TEntity : class, IEntity, new()
    {
        private IServiceBase<TDTO, TEntity> service;

        public BaseController(IServiceBase<TDTO, TEntity> service)
        {
            this.service = service;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] TDTO dto)
        {
            try
            {
                var result = await service.Add(dto);
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
                var result = await service.Delete(id);
                if (result == 0)
                {
                    return StatusCode(404, null);
                }
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
                var result = await service.GetAll();
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
                var result = await service.GetById(id);
                if (result == null)
                {
                    return StatusCode(404, null);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] TDTO dto)
        {
            try
            {
                var result = await service.Update(dto);
                if (result == 0)
                {
                    return StatusCode(404, null);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}