using System.Collections.Generic;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.Entities;

namespace Cadastro.Domain.Abstraction.Repositories
{
    public interface IRepositoryBase<T>
        where T : class, IEntity, new()
    {
        Task<int> Add(T entity);

        Task<int> Delete(int id);

        Task<List<T>> GetAll();

        Task<T> GetById(int id);

        Task<int> Update(T entity);
    }
}