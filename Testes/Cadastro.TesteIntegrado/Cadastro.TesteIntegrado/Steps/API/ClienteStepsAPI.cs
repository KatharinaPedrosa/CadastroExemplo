using Cadastro.Domain.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace Cadastro.TesteIntegrado.Steps.API
{
    [Binding]
    public class ClienteSteps
    {
        private readonly Contexto contexto;
        private readonly HttpClient httpClient;

        public ClienteSteps(Contexto contexto)
        {
            this.contexto = contexto;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1501");
        }

        [When(@"insert a client with the following data")]
        public void WhenInsertAClientWithTheFollowingData(Table table)
        {
            Client cliente = new Client
            {
                Name = table.Rows[0][1],
                CPF = table.Rows[1][1],
                Occupation = table.Rows[2][1],
                DateOfBirth = DateTime.Parse(table.Rows[3][1]),
                PhoneNumber = table.Rows[4][1],
                Address = new Address
                {
                    Street = table.Rows[5][1],
                    Number = table.Rows[6][1],
                    Complement = table.Rows[7][1],
                    Neighborhood = table.Rows[8][1],
                    City = table.Rows[9][1],
                    State = table.Rows[10][1],
                    Country = table.Rows[11][1]
                }
            };

            var content = JsonContent.Create(cliente);

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta = httpClient
               .PutAsync($"/Client", content).Result;
        }

        [Then(@"a client with name ""(.*)"" and cpf ""(.*)"" was inserted")]
        public void ThenAClientWithNameAndCpfWasInserted(string name, string cpf)
        {
            contexto.UsuarioLogado
                .Should()
                .NotBeNull("Porque o usuário deve estar logado");

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta = httpClient
               .GetAsync($"/Client").Result;

            contexto.Resposta.IsSuccessStatusCode
                .Should()
                .BeTrue("Porque o retorno deve ser 200");

            var clientes = contexto.Resposta.Content.ReadFromJsonAsync<List<Client>>().Result;

            clientes.Any(c => c.Name.Equals(name) && c.CPF.Equals(cpf))
                .Should()
                .BeTrue("Porque o cliente deve ser encontrado");
        }

        [Then(@"get the id of the inserted client with name ""(.*)""")]
        [Then(@"a client with name ""(.*)"" it's present on the database")]
        public void ThenGetTheIdOfTheInsertedClientWithName(string name)
        {
            contexto.UsuarioLogado
                .Should()
                .NotBeNull("Porque o usuário deve estar logado");

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta = httpClient
               .GetAsync($"/Client").Result;

            contexto.Resposta.IsSuccessStatusCode
                .Should()
                .BeTrue("Porque o retorno deve ser 200");

            var clientes = contexto.Resposta.Content.ReadFromJsonAsync<List<Client>>().Result;

            contexto.ClienteEncontrado = clientes.FirstOrDefault(c => c.Name.Equals(name));

            contexto.ClienteEncontrado.Should().NotBeNull("Porque o cliente deve ser encontrado");
        }

        [Then(@"find a client by id")]
        public void ThenFindAClientById()
        {
            contexto.UsuarioLogado
               .Should()
               .NotBeNull("Porque o usuário deve estar logado");

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta = httpClient
               .GetAsync($"/Client/{contexto.ClienteEncontrado.Id}").Result;

            contexto.Resposta.IsSuccessStatusCode
                .Should()
                .BeTrue("Porque o retorno deve ser 200");

            var cliente = contexto.Resposta.Content.ReadFromJsonAsync<Client>().Result;

            cliente.Should().NotBeNull("Porque o cliente deve ser encontrado");

            cliente.Id.Should().Be(contexto.ClienteEncontrado.Id, "Porque deve retornar o cliente com o id que foi consultado");

            cliente.Name.Should().Be(contexto.ClienteEncontrado.Name, "Porque deve retornar o cliente com o nome que foi inserido");
        }

        [Then(@"send a request to delete the found client")]
        public void ThenSendARequestToDeleteTheFoundClient()
        {
            contexto.UsuarioLogado
               .Should()
               .NotBeNull("Porque o usuário deve estar logado");

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta =
                    httpClient.DeleteAsync($"/Client/{contexto.ClienteEncontrado.Id}").Result;
        }

        [Then(@"a client with name ""(.*)"" it's not present on the database")]
        public void ThenAClientWithNameItSNotPresentOnTheDatabase(string name)
        {
            contexto.UsuarioLogado
                .Should()
                .NotBeNull("Porque o usuário deve estar logado");

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

            contexto.Resposta = httpClient
               .GetAsync($"/Client").Result;

            contexto.Resposta.IsSuccessStatusCode
                .Should()
                .BeTrue("Porque o retorno deve ser 200");

            var clientes = contexto.Resposta.Content.ReadFromJsonAsync<List<Client>>().Result;

            contexto.ClienteEncontrado = clientes.FirstOrDefault(c => c.Name.Equals(name));

            contexto.ClienteEncontrado.Should().BeNull("Porque o cliente não deve ser encontrado");
        }
    }
}