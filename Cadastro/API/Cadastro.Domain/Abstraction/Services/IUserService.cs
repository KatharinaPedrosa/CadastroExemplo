using System.Collections.Generic;
using System.Threading.Tasks;
using Cadastro.Domain.DTOs;

namespace Cadastro.Domain.Abstraction.Services
{
    public interface IUserService
    {
        Task<User> Login(User user);

        Task<IList<User>> GetUsers();

        Task<User> GetUser(int id);

        Task<int> AddUser(User user);

        Task<int> UpdateUser(User user);

        Task<int> DeleteUser(int id);
    }
}