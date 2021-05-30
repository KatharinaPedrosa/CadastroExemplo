using System;
using System.Threading.Tasks;
using AutoMapper;
using Cadastro.Domain.Abstraction.Repositories;
using Cadastro.Domain.Abstraction.Services;
using Cadastro.Domain.DTOs;
using Cadastro.Domain.Entities;
using Cadastro.Domain.Helpers;
using Microsoft.Extensions.Configuration;

namespace Cadastro.Services
{
    public class UserService : ServiceBase<User, UserEntity>, IUserService
    {
        private IUserRepository repository;
        private IConfiguration config;

        public UserService(
            IUserRepository repository,
            IMapper mapper,
            IConfiguration config)
            : base(repository, mapper)
        {
            this.repository = repository;
            this.config = config;
        }

        public override async Task<int> Delete(int id)
        {
            var user = await repository.GetById(id);
            if (!user.IsAdmin)
            {
                return await base.Delete(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Não é possível deletar o usuário 'admin'");
            }
        }

        public async Task<User> Login(User user)
        {
            var entity = await repository.GetByLogin(user.Login);
            if (entity != null && entity.PasswordHash.Equals(user.PasswordHash, StringComparison.InvariantCultureIgnoreCase))
            {
                User result = mapper.Map<User>(entity);
                result.Token = AuthHelper.GetToken(result,
                    config.GetSection("Auth")["Secret"],
                    config.GetSection("Auth")["Issuer"]);
                return result;
            }
            else
            {
                throw new UnauthorizedAccessException(user.Login);
            }
        }
    }
}