using System.Net.Http.Json;
using System.Threading.Tasks;
using Cadastro.Abstractions.Services;
using Cadastro.Domain.Configuration;
using Cadastro.Domain.DTOs;
using Cadastro.Extenssions;

namespace Cadastro.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(ApiConfiguration apiConfiguration)
            : base(apiConfiguration, "User")
        {
        }

        public async Task<User> Login(string login, string passwordHash)
        {
            var user = new User() { Login = login, PasswordHash = passwordHash };
            var content = JsonContent.Create(user);
            var response = await httpClient
               .PostAsync($"{api}/login", content);
            return await response.CheckResult<User>();
        }
    }
}