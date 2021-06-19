using Cadastro.Domain.DTOs;
using OpenQA.Selenium;
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

        public IWebDriver WebDriver { get; set; }
    }
}