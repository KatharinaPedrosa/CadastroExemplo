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
    public class ClientController : BaseController<Client, ClientEntity>
    {
        public ClientController(IClientService service)
            : base(service)
        {
        }
    }
}