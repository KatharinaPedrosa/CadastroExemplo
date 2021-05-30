using System.Threading.Tasks;
using Cadastro.Domain.DTOs;

namespace Cadastro.Abstractions.Services
{
    public interface IUserService : IServiceBase<User>
    {
        Task<User> Login(string login, string passwordHash);
    }
}