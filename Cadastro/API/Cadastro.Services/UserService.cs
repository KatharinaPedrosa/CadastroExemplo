using System;
using System.Collections.Generic;
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
    public class UserService : IUserService
    {
        private IUserRepository repository;
        private IMapper mapper;
        private IConfiguration config;

        public UserService(
            IUserRepository repository,
            IMapper mapper,
            IConfiguration config)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.config = config;
        }

        public Task<int> AddUser(User user)
        {
            var entity = mapper.Map<UserEntity>(user);
            return repository.Add(entity);
        }

        public Task<int> DeleteUser(int id)
        {
            return repository.Delete(id);
        }

        public async Task<User> GetUser(int id)
        {
            var entity = await repository.GetById(id);
            return mapper.Map<User>(entity);
        }

        public async Task<IList<User>> GetUsers()
        {
            var entities = await repository.GetAll();
            return mapper.Map<List<User>>(entities);
        }

        public async Task<User> Login(User user)
        {
            var entity = await repository.GetByLogin(user.Login);
            if (entity != null && entity.PasswordHash.Equals(user.PasswordHash, StringComparison.InvariantCultureIgnoreCase))
            {
                User result = mapper.Map<User>(user);
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

        public Task<int> UpdateUser(User user)
        {
            var entity = mapper.Map<UserEntity>(user);
            return repository.Update(entity);
        }
    }
}