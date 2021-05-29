using System.Collections.Generic;
using System.Threading.Tasks;
using Cadastro.Domain.Entities;

namespace Cadastro.Domain.Abstraction.Repositories
{
    public interface IClientRepository
    {
        Task<int> Add(ClientEntity entity);

        Task<int> Delete(int id);

        Task<List<ClientEntity>> GetAll();

        Task<ClientEntity> GetById(int id);

        Task<int> Update(ClientEntity entity);
    }
}