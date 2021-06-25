using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Cadastro.Domain.DTOs;
using OpenQA.Selenium.Html5;
using System.Text.Json;
using OpenQA.Selenium.Support.UI;

namespace Cadastro.TesteIntegrado.Steps.APP
{
    [Binding]
    internal class GeneralAPPSteps
    {
        private readonly Contexto contexto;

        public GeneralAPPSteps(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [Before("APP")]
        public void AbrirNavegador()
        {
            contexto.WebDriver = new ChromeDriver("Driver");
            contexto.WebDriver.Navigate().GoToUrl("http://localhost:1501/");
            contexto.WebDriverWait = new WebDriverWait(contexto.WebDriver, TimeSpan.FromSeconds(10));
            contexto.Wait("SignIn");
        }

        [When(@"I type ""(.*)"" on ""(.*)"" field")]
        public void WhenITypeOnField(string value, string field)
        {
            var campo = contexto.WebDriver.FindElement(By.Id(field));
            campo.SendKeys(value);
        }

        [When(@"I click on (button|menu) ""(.*)""")]
        [Given(@"that I'm on (button|menu) ""(.*)""")]
        public void WhenIClickOnButton(string tipo, string valor)
        {
            var tag = "";
            switch (tipo)
            {
                case "button":
                    tag = "button";
                    break;

                case "menu":
                    tag = "a";
                    break;

                default:
                    tag = "div";
                    break;
            }
            var botao = contexto.WebDriver.FindElements(By.Id(valor)).FirstOrDefault();
            if (botao == null)
            {
                botao = contexto.WebDriver.FindElements(By.TagName(tag))
                    .FirstOrDefault(b => b.Text.Equals(valor));
            }
            botao.Click();
        }

        [Given(@"That I'm logged on the app, with user ""(.*)"" and password ""(.*)""")]
        public void GivenThatIMLoggedOnTheAppWithUserAndPassword(string user, string password)
        {
            WhenITypeOnField(user, "Login");
            WhenITypeOnField(password, "Password");
            WhenIClickOnButton("button", "SignIn");
            contexto.Wait("SignOut");
            var loggedUserString = contexto.WebDriver
                .ExecuteScript("return localStorage.getItem('LoggedUser');")
                .ToString();
            contexto.UsuarioLogado = JsonSerializer.Deserialize<User>(loggedUserString);
        }

        [Then(@"the (?:label|field) ""(.*)"" shows ""(.*)""")]
        public void ThenTheLabelShows(string field, string value)
        {
            try
            {
                contexto.Wait(field);
                var elemento = contexto.WebDriver.FindElement(By.Id(field));
                elemento.Text.Should().Be(value, "porque o valor deve ser encontrado");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Elemento não encontrado");
            }
        }

        [When(@"I fill the fields with the following data")]
        public void WhenIFillTheFieldsWithTheFollowingData(Table table)
        {
            foreach (var linha in table.Rows)
            {
                var campo = contexto.WebDriver.FindElement(By.Id(linha[0]));
                campo.SendKeys(linha[1]);
            }
        }

        [After("APP")]
        public void FecharNavegador()
        {
            if (contexto.WebDriver != null)
            {
                contexto.WebDriver.ExecuteScript("localStorage.removeItem('LoggedUser')");
                contexto.WebDriver.Quit();
                contexto.WebDriver = null;
            }
        }
    }
}