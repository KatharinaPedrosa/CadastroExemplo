using Cadastro.Abstractions.Services;
using Cadastro.Domain.Configuration;
using Cadastro.Domain.DTOs;

namespace Cadastro.Services
{
    public class ClientService : ServiceBase<Client>, IClientService
    {
        public ClientService(ApiConfiguration apiConfiguration)
            : base(apiConfiguration, "Client")
        {
        }
    }
}