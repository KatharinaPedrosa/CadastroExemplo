using System.Collections.Generic;
using System.Threading.Tasks;
using Cadastro.Domain.DTOs;

namespace Cadastro.Domain.Abstraction.Services
{
    public interface IClientService
    {
        Task<IList<Client>> GetClients();

        Task<Client> GetClient(int id);

        Task<int> AddClient(Client client);

        Task<int> UpdateClient(Client client);

        Task<int> DeleteClient(int id);
    }
}