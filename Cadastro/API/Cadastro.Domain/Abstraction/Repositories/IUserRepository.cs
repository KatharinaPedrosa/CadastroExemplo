using System.Threading.Tasks;
using Cadastro.Domain.Entities;

namespace Cadastro.Domain.Abstraction.Repositories
{
    public interface IUserRepository : IRepositoryBase<UserEntity>
    {
        Task<UserEntity> GetByLogin(string login);
    }
}