using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cadastro.Abstractions.Helpers;
using Cadastro.Abstractions.Services;
using Cadastro.Domain.Configuration;
using Cadastro.Helpers;
using Cadastro.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadastro
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                return config.GetSection("CadastroApi").Get<ApiConfiguration>();
            });

            services.AddSingleton<IUserService, ClientUserService>();
            services.AddSingleton<IClientService, ClientClientService>();
            services.AddSingleton<IHashHelper, HashHelper>();
            services.AddSingleton<ILocalStorageHelper, LocalStorageHelper>();
            services.AddSingleton<ICadastroService, CadastroService>();
        }
    }
}