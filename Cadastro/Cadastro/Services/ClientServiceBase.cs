using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cadastro.Abstractions.Services;
using Cadastro.Domain.Abstraction.DTOs;
using Cadastro.Domain.Configuration;
using Cadastro.Extenssions;

namespace Cadastro.Services
{
    public class ClientServiceBase<T> : IServiceBase<T>
        where T : class, IDTO, new()
    {
        protected HttpClient httpClient;
        protected string api;

        public ClientServiceBase(ApiConfiguration apiConfiguration, string api)
        {
            this.api = api;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiConfiguration.Url);
        }

        public virtual async Task<bool> Add(T dto, string token)
        {
            var content = JsonContent.Create(dto);
            var response = await httpClient
                .AddJWT(token)
                .PostAsync(api, content);
            return response.CheckResult();
        }

        public virtual async Task<bool> Delete(int id, string token)
        {
            var response = await httpClient
                .AddJWT(token)
                .DeleteAsync($"{api}/{id}");
            return response.CheckResult();
        }

        public virtual async Task<List<T>> Get(string token)
        {
            var response = await httpClient
                .AddJWT(token)
                .GetAsync(api);
            return await response.CheckResult<List<T>>();
        }

        public virtual async Task<T> Get(int id, string token)
        {
            var response = await httpClient
                .AddJWT(token)
                .GetAsync($"{api}/{id}");
            return await response.CheckResult<T>();
        }

        public virtual async Task<bool> Update(T dto, string token)
        {
            var content = JsonContent.Create(dto);
            var response = await httpClient
               .AddJWT(token)
               .PutAsync(api, content);
            return response.CheckResult();
        }
    }
}