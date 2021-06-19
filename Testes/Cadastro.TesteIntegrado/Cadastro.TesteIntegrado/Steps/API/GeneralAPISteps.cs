using Cadastro.Domain.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace Cadastro.TesteIntegrado.Steps.API
{
    [Binding]
    public class GeneralAPISteps
    {
        private readonly Contexto contexto;
        private readonly HttpClient httpClient;

        public GeneralAPISteps(Contexto contexto)
        {
            this.contexto = contexto;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1501");
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int status)
        {
            contexto.Resposta.Should().NotBeNull("Não há resposta");
            contexto.Resposta.StatusCode.Should().Be((int)contexto.Resposta.StatusCode, "Resultado não confere");
        }

        [After]
        public void CleanScenario()
        {
            if (contexto.UsuarioLogado == null) return;

            httpClient.DefaultRequestHeaders.Authorization =
              new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta = httpClient
               .GetAsync($"/Client").Result;

            if (contexto.Resposta.IsSuccessStatusCode)
            {
                var clientes = contexto.Resposta.Content.ReadFromJsonAsync<List<Client>>().Result;
                foreach (var cliente in clientes)
                {
                    contexto.Resposta =
                    httpClient.DeleteAsync($"/Client/{cliente.Id}").Result;
                }
            }

            contexto.Resposta = httpClient
               .GetAsync($"/User").Result;

            if (contexto.Resposta.IsSuccessStatusCode)
            {
                var usuarios = contexto.Resposta.Content.ReadFromJsonAsync<List<User>>().Result;
                foreach (var usuario in usuarios.Where(u => !u.IsAdmin))
                {
                    contexto.Resposta =
                    httpClient.DeleteAsync($"/User/{usuario.Id}").Result;
                }
            }
        }
    }
}