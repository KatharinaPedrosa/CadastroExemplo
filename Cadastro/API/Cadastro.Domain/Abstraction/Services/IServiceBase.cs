using System.Collections.Generic;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.DTOs;
using Cadastro.Domain.Abstraction.Entities;

namespace Cadastro.Domain.Abstraction.Services
{
    public interface IServiceBase<TDTO, TEntity>
        where TDTO : class, IDTO, new()
        where TEntity : class, IEntity, new()
    {
        Task<IList<TDTO>> GetAll();

        Task<TDTO> GetById(int id);

        Task<int> Add(TDTO dto);

        Task<int> Update(TDTO dto);

        Task<int> Delete(int id);
    }
}