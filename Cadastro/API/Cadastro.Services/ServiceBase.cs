using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cadastro.Domain.Abstraction.DTOs;
using Cadastro.Domain.Abstraction.Entities;
using Cadastro.Domain.Abstraction.Repositories;
using Cadastro.Domain.Abstraction.Services;

namespace Cadastro.Services
{
    public class ServiceBase<TDTO, TEntity> : IServiceBase<TDTO, TEntity>
        where TDTO : class, IDTO, new()
        where TEntity : class, IEntity, new()
    {
        private IRepositoryBase<TEntity> repository;
        protected IMapper mapper;

        public ServiceBase(IRepositoryBase<TEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual Task<int> Add(TDTO dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            return repository.Add(entity);
        }

        public virtual Task<int> Delete(int id)
        {
            return repository.Delete(id);
        }

        public virtual async Task<IList<TDTO>> GetAll()
        {
            var entity = await repository.GetAll();
            return mapper.Map<List<TDTO>>(entity);
        }

        public virtual async Task<TDTO> GetById(int id)
        {
            var entity = await repository.GetById(id);
            return mapper.Map<TDTO>(entity);
        }

        public virtual Task<int> Update(TDTO dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            return repository.Update(entity);
        }
    }
}