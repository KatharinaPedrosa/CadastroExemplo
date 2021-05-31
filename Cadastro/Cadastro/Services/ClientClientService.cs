using Cadastro.Abstractions.Services;
using Cadastro.Domain.Configuration;
using Cadastro.Domain.DTOs;

namespace Cadastro.Services
{
    public class ClientClientService : ClientServiceBase<Client>, IClientService
    {
        public ClientClientService(ApiConfiguration apiConfiguration)
            : base(apiConfiguration, "Client")
        {
        }
    }
}