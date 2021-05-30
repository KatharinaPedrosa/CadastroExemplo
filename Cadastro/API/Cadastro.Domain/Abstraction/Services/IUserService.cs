using System.Threading.Tasks;
using Cadastro.Domain.DTOs;
using Cadastro.Domain.Entities;

namespace Cadastro.Domain.Abstraction.Services
{
    public interface IUserService : IServiceBase<User, UserEntity>
    {
        Task<User> Login(User user);
    }
}