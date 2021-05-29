using System;
using System.Threading.Tasks;
using Cadastro.Domain.Abstraction.Repositories;
using Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cadastro.Repositories
{
    public class Context : DbContext, IContext
    {
        private readonly IConfiguration config;

        public Context(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={config.GetSection("DataBase")["Server"]}", b => b.MigrationsAssembly("Cadastro.API"));
            optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .ToTable("Users")
                .HasKey(t => t.Id);

            modelBuilder.Entity<AddressEntity>()
                .ToTable("Addresses")
                .HasKey(t => t.Id);

            modelBuilder.Entity<ClientEntity>()
                .ToTable("Clients")
                .HasKey(t => t.Id);

            modelBuilder.Entity<ClientEntity>()
                .HasOne(t => t.Address)
                .WithOne(t => t.Client)
                .HasForeignKey<AddressEntity>(t => t.ClientId);

            modelBuilder.Entity<UserEntity>()
                .HasData(
                    new UserEntity() { Id = 1, Login = "admin", PasswordHash = "21232f297a57a5a743894a0e4a801fc3" }
                );
        }

        public void Atach<T>(T entity) where T : class
        {
            base.Attach(entity);
        }

        public void Deatach<T>(T entity) where T : class
        {
            base.Entry(entity).State = EntityState.Detached;
        }

        public DbSet<T> GetSet<T>() where T : class
        {
            return Set<T>();
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}