using Cadastro.Domain.DTOs;
using Cadastro.Domain.Helpers;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using TechTalk.SpecFlow;

namespace Cadastro.TesteIntegrado.Steps.APP
{
    [Binding]
    public class UsuarioAPPSteps
    {
        private readonly Contexto contexto;
        private readonly HttpClient httpClient;

        public UsuarioAPPSteps(Contexto contexto)
        {
            this.contexto = contexto;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1501");
        }

        [Then(@"the user with login ""(.*)"" is on the user grid")]
        public void ThenTheUserWithLoginIsOnTheUserGrid(string login)
        {
            Thread.Sleep(1000);
            var encontrou = false;
            var linhas =
                contexto.WebDriver
                .FindElements(By.XPath("//div[@class='main']/div[2]/div[@class='container']//div"))
                .Where(linha => !linha.GetAttribute("class").Contains("cabecalho"));
            foreach (var linha in linhas)
            {
                var campoLogin = linha.FindElement(By.XPath(".//span[2]"));

                if (campoLogin.Text.Equals(login))
                {
                    encontrou = true;
                    break;
                }
            }
            encontrou.Should().BeTrue("porque os campos devem ser encontrados");
        }

        [Then(@"the menu ""(.*)"" it's not shown")]
        [Then(@"the button ""(.*)"" it's not shown")]
        public void ThenTheMenuItSNotShown(string menu)
        {
            var menus = contexto.WebDriver.FindElements(By.Id(menu));
            menus.Should().BeEmpty();
        }

        [Given(@"that the following users data are on the database")]
        public void GivenThatTheFollowingUsersDataAreOnTheDatabase(Table usuarios)
        {
            foreach (var linha in usuarios.Rows)
            {
                var usuario = new User
                {
                    Login = linha[0],
                    PasswordHash = AuthHelper.GetMd5Hash(linha[1])
                };
                var content = JsonContent.Create(usuario);

                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", contexto.UsuarioLogado.Token);

                contexto.Resposta = httpClient
                   .PutAsync($"/User", content).Result;
            }
        }

        [When(@"I click on (.*) on user ""(.*)""")]
        public void WhenIClickOnEditOnUser(string button, string user)
        {
            var linhas =
                contexto.WebDriver
                .FindElements(By.XPath("//div[@class='main']/div[2]/div[@class='container']//div"))
                .Where(linha => !linha.GetAttribute("class").Contains("cabecalho"));
            foreach (var linha in linhas)
            {
                var campoUsuario = linha.FindElement(By.XPath(".//span[2]"));

                if (campoUsuario.Text != user) continue;

                if (button == "edit")
                {
                    var campoEdit = linha.FindElement(By.XPath(".//span[3]"));
                    campoEdit.Click();
                }
                else
                {
                    var campoRemove = linha.FindElement(By.XPath(".//span[4]"));
                    campoRemove.Click();
                }

                break;
            }
        }
    }
}