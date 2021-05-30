using System.Linq;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.Repositories;
using Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(IContext context) : base(context)
        {
        }

        public Task<UserEntity> GetByLogin(string login)
        {
            return dbSet
                .Where(t => t.Login == login)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}