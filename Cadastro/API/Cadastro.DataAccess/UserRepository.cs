using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.Repositories;
using Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected DbSet<UserEntity> dbSet;

        protected IContext context;

        public UserRepository(IContext context)
        {
            this.context = context;
            dbSet = context.GetSet<UserEntity>();
        }

        public async Task<int> Add(UserEntity entity)
        {
            dbSet.Add(entity);
            var result = await context.SaveChangesAsync();
            context.Deatach(entity);
            return result;
        }

        public Task<int> Delete(int id)
        {
            dbSet.Remove(new UserEntity() { Id = id });
            return context.SaveChangesAsync();
        }

        public Task<List<UserEntity>> GetAll()
        {
            return dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<UserEntity> GetById(int id)
        {
            return dbSet
                .Where(t => t.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public Task<UserEntity> GetByLogin(string login)
        {
            return dbSet
                .Where(t => t.Login.ToUpperInvariant() == login.ToUpperInvariant())
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<int> Update(UserEntity entity)
        {
            dbSet.Update(entity);
            var result = await context.SaveChangesAsync();
            context.Deatach(entity);
            return result;
        }
    }
}