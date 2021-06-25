using Cadastro.Domain.DTOs;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.TesteIntegrado
{
    public class Contexto
    {
        public User UsuarioLogado { get; set; }

        public HttpResponseMessage Resposta { get; set; }

        public Client ClienteEncontrado { get; set; }

        public User UsuarioEncontrado { get; set; }

        public ChromeDriver WebDriver { get; set; }

        public WebDriverWait WebDriverWait { get; set; }

        public void Wait(string id)
        {
            if (WebDriverWait != null)
            {
                WebDriverWait.Until(driver => driver.FindElements(By.Id(id)).Any());
            }
        }
    }
}