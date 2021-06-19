using Cadastro.Domain.DTOs;
using Cadastro.Domain.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace Cadastro.TesteIntegrado.Steps.API
{
    [Binding]
    public class LoginStepsAPI
    {
        private readonly Contexto contexto;
        private readonly HttpClient httpClient;

        public LoginStepsAPI(Contexto contexto)
        {
            this.contexto = contexto;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1501");
        }

        [Given(@"I'm not logged on the API")]
        public void GivenIMNotLoggedOnTheAPI()
        {
            contexto.UsuarioLogado = null;
        }

        [Given(@"I'm logged on the API, with the user ""(.*)"" and the password ""(.*)""")]
        [When(@"I'm send a login request with the user ""(.*)"" and the password ""(.*)""")]
        public void APILogin(string usuario, string senha)
        {
            var user = new User() { Login = usuario, PasswordHash = AuthHelper.GetMd5Hash(senha) };
            var content = JsonContent.Create(user);
            contexto.Resposta = httpClient
               .PostAsync($"/User/login", content).Result;

            if (contexto.Resposta != null && contexto.Resposta.IsSuccessStatusCode)
            {
                contexto.UsuarioLogado = contexto.Resposta.Content.ReadFromJsonAsync<User>().Result;
            }
            else
            {
                contexto.UsuarioLogado = null;
            }
        }

        [Then(@"the logged user is a system administrator on the API")]
        public void ThenTheLoggedUserIsASystemAdministrator()
        {
            contexto.UsuarioLogado.Should().NotBeNull("Usuário não logado");
            contexto.UsuarioLogado.IsAdmin.Should().BeTrue("Usuário não é administrador");
        }
    }
}