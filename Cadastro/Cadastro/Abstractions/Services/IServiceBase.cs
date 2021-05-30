using System.Collections.Generic;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.DTOs;

namespace Cadastro.Abstractions.Services
{
    public interface IServiceBase<T>
        where T : class, IDTO, new()
    {
        Task<bool> Add(T dto, string token);

        Task<bool> Delete(int id, string token);

        Task<List<T>> Get(string token);

        Task<T> Get(int id, string token);

        Task<bool> Update(T dto, string token);
    }
}