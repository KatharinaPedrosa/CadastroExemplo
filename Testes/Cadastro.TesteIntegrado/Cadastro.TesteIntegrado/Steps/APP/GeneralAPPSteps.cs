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
            Thread.Sleep(2000);
        }

        [When(@"I type ""(.*)"" on ""(.*)"" field")]
        public void WhenITypeOnField(string value, string field)
        {
            var campo = contexto.WebDriver.FindElement(By.Id(field));
            campo.SendKeys(value);
        }

        [When(@"I click on (?:button|menu) ""(.*)""")]
        public void WhenIClickOnButton(string button)
        {
            var botao = contexto.WebDriver.FindElement(By.Id(button));
            botao.Click();
            Thread.Sleep(2000);
        }

        [Given(@"That I'm logged on the app, with user ""(.*)"" and password ""(.*)""")]
        public void GivenThatIMLoggedOnTheAppWithUserAndPassword(string user, string password)
        {
            WhenITypeOnField(user, "Login");
            WhenITypeOnField(password, "Password");
            WhenIClickOnButton("SignIn");
        }

        [Then(@"the (?:label|field) ""(.*)"" shows ""(.*)""")]
        public void ThenTheLabelShows(string field, string value)
        {
            try
            {
                var elemento = contexto.WebDriver.FindElement(By.Id(field));
                elemento.Text.Should().Be(value, "porque o valor deve ser encontrado");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Elemento não encontrado");
            }
        }

        [After("APP")]
        public void FecharNavegador()
        {
            if (contexto.WebDriver != null)
            {
                contexto.WebDriver.Quit();
                contexto.WebDriver = null;
            }
        }
    }
}