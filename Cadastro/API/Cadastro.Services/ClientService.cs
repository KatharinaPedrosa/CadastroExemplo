using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cadastro.Domain.Abstraction.Repositories;
using Cadastro.Domain.Abstraction.Services;
using Cadastro.Domain.DTOs;
using Cadastro.Domain.Entities;

namespace Cadastro.Services
{
    public class ClientService : IClientService
    {
        private IClientRepository repository;
        private IMapper mapper;

        public ClientService(IClientRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task<int> AddClient(Client client)
        {
            var entity = mapper.Map<ClientEntity>(client);
            return repository.Add(entity);
        }

        public Task<int> DeleteClient(int id)
        {
            return repository.Delete(id);
        }

        public async Task<Client> GetClient(int id)
        {
            var entity = await repository.GetById(id);
            return mapper.Map<Client>(entity);
        }

        public async Task<IList<Client>> GetClients()
        {
            var entities = await repository.GetAll();
            return mapper.Map<List<Client>>(entities);
        }

        public Task<int> UpdateClient(Client client)
        {
            var entity = mapper.Map<ClientEntity>(client);
            return repository.Update(entity);
        }
    }
}