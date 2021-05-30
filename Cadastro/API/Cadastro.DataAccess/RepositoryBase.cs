using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.Entities;
using Cadastro.Domain.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class, IEntity, new()
    {
        protected DbSet<T> dbSet;

        protected IContext context;

        public RepositoryBase(IContext context)
        {
            this.context = context;
            dbSet = context.GetSet<T>();
        }

        public virtual async Task<int> Add(T entity)
        {
            dbSet.Add(entity);
            var result = await context.SaveChangesAsync();
            context.Deatach(entity);
            return result;
        }

        public virtual Task<int> Delete(int id)
        {
            dbSet.Remove(new T() { Id = id });
            return context.SaveChangesAsync();
        }

        public virtual Task<List<T>> GetAll()
        {
            return dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual Task<T> GetById(int id)
        {
            return dbSet
                .Where(t => t.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public virtual async Task<int> Update(T entity)
        {
            dbSet.Update(entity);
            var result = await context.SaveChangesAsync();
            context.Deatach(entity);
            return result;
        }
    }
}