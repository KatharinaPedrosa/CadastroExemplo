using System.Collections.Generic;
using System.Threading.Tasks;
using Cadastro.Domain.Entities;

namespace Cadastro.Domain.Abstraction.Repositories
{
    public interface IUserRepository
    {
        Task<int> Add(UserEntity entity);

        Task<int> Delete(int id);

        Task<List<UserEntity>> GetAll();

        Task<UserEntity> GetById(int id);

        Task<UserEntity> GetByLogin(string login);

        Task<int> Update(UserEntity entity);
    }
}