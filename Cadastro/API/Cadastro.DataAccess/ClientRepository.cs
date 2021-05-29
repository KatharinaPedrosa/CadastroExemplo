using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.Repositories;
using Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Repositories
{
    public class ClientRepository : IClientRepository
    {
        protected DbSet<ClientEntity> dbSet;

        protected IContext context;

        public ClientRepository(IContext context)
        {
            this.context = context;
            dbSet = context.GetSet<ClientEntity>();
        }

        public async Task<int> Add(ClientEntity entity)
        {
            dbSet.Add(entity);
            var result = await context.SaveChangesAsync();
            context.Deatach(entity);
            return result;
        }

        public Task<int> Delete(int id)
        {
            dbSet.Remove(new ClientEntity() { Id = id });
            return context.SaveChangesAsync();
        }

        public Task<List<ClientEntity>> GetAll()
        {
            return dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<ClientEntity> GetById(int id)
        {
            return dbSet
                .Where(t => t.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<int> Update(ClientEntity entity)
        {
            dbSet.Update(entity);
            var result = await context.SaveChangesAsync();
            context.Deatach(entity);
            return result;
        }
    }
}