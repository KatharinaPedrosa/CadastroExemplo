using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace Cadastro.TesteIntegrado.Steps.APP
{
    [Binding]
    public class LoginStepsAPP
    {
        private readonly Contexto contexto;

        public LoginStepsAPP(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [Given(@"That I'm not logged on the app")]
        public void GivenThatIMNotLoggedOnTheApp()
        {
            try
            {
                var signOut = contexto.WebDriver.FindElement(By.Id("SignOut"));
                signOut.Click();
            }
            catch (NoSuchElementException)
            {
            }
        }

        [Then(@"the login was successful")]
        public void ThenTheLoginWasSuccessful()
        {
            try
            {
                contexto.Wait("SignOut");
                var signOut = contexto.WebDriver.FindElement(By.Id("SignOut"));
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Login não efetuado");
            }
        }

        [Then(@"the logged user ""(.*)"" it's shown on the top of the screen")]
        public void ThenTheLoggedUserItSShownOnTheTopOfTheScreen(string loggedUser)
        {
            try
            {
                contexto.Wait("SignOut");
                var topRow = contexto.WebDriver.FindElement(By.ClassName("top-row-block"));
                var usuario = topRow.FindElement(By.TagName("label"));
                usuario.Text.Should().Be(loggedUser);
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Usuário não está logado!");
            }
        }

        [Then(@"the login was not successful")]
        public void ThenTheLoginWasNotSuccessful()
        {
            try
            {
                contexto.Wait("LoginMessage");
                var mensagemLogin = contexto.WebDriver.FindElement(By.Id("LoginMessage"));
                mensagemLogin.Text.Should().Be("Usuário ou senha inválidos", "porque o usuário não deve estar logado");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("porque não encontrou a mensagem de falha no login");
            }
        }

        [Then(@"the logout was successful")]
        public void ThenTheLogoutWasSuccessful()
        {
            try
            {
                contexto.Wait("SignIn");
                var botaoSignIn = contexto.WebDriver.FindElement(By.Id("SignIn"));
                botaoSignIn.Should().NotBeNull();
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("LogOut não efetuado");
            }
        }
    }
}