using Cadastro.Domain.DTOs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using TechTalk.SpecFlow;

namespace Cadastro.TesteIntegrado.Steps.APP
{
    [Binding]
    public class ClienteAddAPPSteps
    {
        private readonly Contexto contexto;
        private readonly HttpClient httpClient;

        public ClienteAddAPPSteps(Contexto contexto)
        {
            this.contexto = contexto;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1501");
        }

        [Then(@"the client with name ""(.*)"" and phone number ""(.*)"" is on the client grid")]
        public void ThenTheClientWithNameAndPhoneNumberIsOnTheClientGrid(string nome, string telefone)
        {
            var encontrou = false;
            var linhas =
                contexto.WebDriver
                .FindElements(By.XPath("//div[@class='main']/div[2]/div[@class='container']//div"))
                .Where(linha => !linha.GetAttribute("class").Contains("cabecalho"));
            foreach (var linha in linhas)
            {
                var campoNome = linha.FindElement(By.XPath(".//span[2]"));
                var campoTelefone = linha.FindElement(By.XPath(".//span[3]"));
                if (campoNome.Text.Equals(nome) && campoTelefone.Text.Equals(telefone))
                {
                    encontrou = true;
                    break;
                }
            }
            encontrou.Should().BeTrue("porque os campos devem ser encontrados");
        }

        [Given(@"that the following clients data are on the database")]
        public void ClientesTeste(Table clientes)
        {
            foreach (var linha in clientes.Rows)
            {
                var cliente = new Client
                {
                    Name = linha[0],
                    PhoneNumber = linha[1],
                    CPF = linha[2],
                    DateOfBirth = new DateTime(1990, 05, 24),
                    Occupation = "teste",
                    Address = new Address
                    {
                        Street = "teste",
                        Neighborhood = "Teste",
                        City = "teste",
                        State = "teste",
                        Country = "teste"
                    }
                };
                var content = JsonContent.Create(cliente);

                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

                contexto.Resposta = httpClient
                   .PutAsync($"/Client", content).Result;
            }
            contexto.WebDriver.Navigate().Refresh();
            contexto.Wait("CurrentLocation");
        }

        [Then(@"the grid must have the following clients")]
        public void ThenTheGridMustHaveTheFollowingClients(Table table)
        {
            foreach (var linha in table.Rows)
            {
                ThenTheClientWithNameAndPhoneNumberIsOnTheClientGrid(linha[0], linha[1]);
            }
        }

        [Then(@"the grid must have (.*) clients")]
        public void ThenTheGridMustHaveClients(int quantidade)
        {
            var linhas =
                contexto.WebDriver
                .FindElements(By.XPath("//div[@class='main']/div[2]/div[@class='container']//div"))
                .Where(linha => !linha.GetAttribute("class").Contains("cabecalho"));
            linhas.Should().HaveCount(quantidade);
        }

        [Then(@"the following validation fields are shown")]
        public void ThenTheFollowingValidationFieldsAreShown(Table table)
        {
            var campos = contexto.WebDriver.FindElements(By.ClassName("form-group"));

            foreach (var linha in table.Rows)
            {
                var campo = campos.Where(campo => campo.FindElements(By.Id(linha[0])).Any()).FirstOrDefault();
                var validationMessage = campo.FindElement(By.ClassName("validation-message"));
                validationMessage.Text.Should().Be(linha[1]);
            }
        }

        [When(@"I click on (.*) on client ""(.*)""")]
        public void WhenIClickOnClient(string button, string cliente)
        {
            var linhas =
                contexto.WebDriver
                .FindElements(By.XPath("//div[@class='main']/div[2]/div[@class='container']//div"))
                .Where(linha => !linha.GetAttribute("class").Contains("cabecalho"));
            foreach (var linha in linhas)
            {
                var campoNome = linha.FindElement(By.XPath(".//span[2]"));

                if (campoNome.Text != cliente) continue;

                if (button == "edit")
                {
                    var campoEdit = linha.FindElement(By.XPath(".//span[4]"));
                    campoEdit.Click();
                }
                else
                {
                    var campoRemove = linha.FindElement(By.XPath(".//span[5]"));
                    campoRemove.Click();
                }

                break;
            }
        }
    }
}