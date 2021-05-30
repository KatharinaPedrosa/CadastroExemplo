using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.Repositories;
using Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Repositories
{
    public class ClientRepository : RepositoryBase<ClientEntity>, IClientRepository
    {
        public ClientRepository(IContext context) : base(context)
        {
        }

        public override Task<List<ClientEntity>> GetAll()
        {
            return dbSet
               .Include(t => t.Address)
               .AsNoTracking()
               .ToListAsync();
        }

        public override Task<ClientEntity> GetById(int id)
        {
            return dbSet
                .Where(t => t.Id == id)
                .Include(t => t.Address)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}