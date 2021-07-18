using Cadastro.Domain.DTOs;
using Cadastro.Domain.Helpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using TechTalk.SpecFlow;

namespace Cadastro.TesteIntegrado.Steps.API
{
    [Binding]
    public class UsuarioSteps
    {
        private readonly Contexto contexto;
        private readonly HttpClient httpClient;

        public UsuarioSteps(Contexto contexto)
        {
            this.contexto = contexto;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1501");
        }

        [When(@"insert a user with the following data")]
        public void WhenInsertAUserWithTheFollowingData(Table table)
        {
            var usuario = new User
            {
                Login = table.Rows[0][1],
                PasswordHash = AuthHelper.GetMd5Hash(table.Rows[1][1])
            };
            var content = JsonContent.Create(usuario);

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta = httpClient
               .PutAsync($"/User", content).Result;
        }

        [Then(@"a user with login ""(.*)"" was inserted")]
        public void ThenAUserWithLogin(string login)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta = httpClient
               .GetAsync($"/User").Result;

            var usuarios = contexto.Resposta.Content.ReadFromJsonAsync<List<User>>().Result;

            usuarios.Any(u => u.Login.Equals(login))
                .Should()
                .BeTrue("Porque o usuário deve ser encontrado");
        }

        [Then(@"a user with login ""(.*)"" it's present on the database")]
        public void ThenGetTheIdOfTheInsertedUserWithLogin(string login)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta = httpClient
               .GetAsync($"/User").Result;

            var usuarios = contexto.Resposta.Content.ReadFromJsonAsync<List<User>>().Result;

            contexto.UsuarioEncontrado = usuarios.FirstOrDefault(u => u.Login.Equals(login));

            contexto.UsuarioEncontrado.Should().NotBeNull("Porque o usuário deve ser encontrado");
        }

        [When(@"consult a user with login ""(.*)"" by id")]
        public void ThenConsultAUserById(string login)
        {
            ThenGetTheIdOfTheInsertedUserWithLogin(login);

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta = httpClient
               .GetAsync($"/User/{contexto.UsuarioEncontrado.Id}").Result;

            var usuario = contexto.Resposta.Content.ReadFromJsonAsync<User>().Result;

            usuario.Should().NotBeNull("Porque o usuário deve ser encontrado");

            usuario.Id.Should().Be(contexto.UsuarioEncontrado.Id, "Porque deve retornar o usuário com o id que foi consultado");

            usuario.Login.Should().Be(contexto.UsuarioEncontrado.Login, "Porque deve retornar o usuário com o login que foi inserido");
        }

        [When(@"send a request to delete the user with login ""(.*)""")]
        public void ThenSendARequestToDeleteTheFoundUser(string login)
        {
            ThenGetTheIdOfTheInsertedUserWithLogin(login);

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta =
                    httpClient.DeleteAsync($"/User/{contexto.UsuarioEncontrado.Id}").Result;
        }

        [Then(@"a user with login ""(.*)"" it's not present on the database")]
        public void ThenAUserWithLoginItSNotPresentOnTheDatabase(string login)
        {
            Thread.Sleep(2000);
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta = httpClient
               .GetAsync($"/User").Result;

            var usuarios = contexto.Resposta.Content.ReadFromJsonAsync<List<User>>().Result;

            contexto.UsuarioEncontrado = usuarios.FirstOrDefault(u => u.Login.Equals(login));

            contexto.UsuarioEncontrado.Should().BeNull("Porque o usuário não deve ser encontrado");
        }
    }
}