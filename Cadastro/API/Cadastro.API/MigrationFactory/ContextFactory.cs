﻿using Cadastro.Repositories;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Cadastro.API.MigrationFactory
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var resourceName = "appsettings.json";
            var config = new ConfigurationBuilder().AddJsonFile(resourceName).Build();
            return new Context(config);
        }
    }
}