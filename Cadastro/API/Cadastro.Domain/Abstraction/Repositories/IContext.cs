﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Domain.Abstraction.Repositories
{
    public interface IContext
    {
        public DbSet<T> GetSet<T>() where T : class;

        public Task<int> SaveChangesAsync();

        public void Atach<T>(T entity) where T : class;

        public void Deatach<T>(T entity) where T : class;
    }
}