using AutoMapper;
using Cadastro.Domain.Abstraction.Repositories;
using Cadastro.Domain.Abstraction.Services;
using Cadastro.Domain.DTOs;
using Cadastro.Domain.Entities;

namespace Cadastro.Services
{
    public class ClientService : ServiceBase<Client, ClientEntity>, IClientService
    {
        public ClientService(IClientRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}