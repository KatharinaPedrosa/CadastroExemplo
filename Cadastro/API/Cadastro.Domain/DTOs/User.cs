using System.ComponentModel.DataAnnotations;
using Cadastro.Domain.Abstraction.DTOs;

namespace Cadastro.Domain.DTOs
{
    public class User : IDTO
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Login obrigatório")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Senha obrigatória")]
        public string PasswordHash { get; set; }

        public string NewPasswordHash { get; set; }

        public string Token { get; set; }

        public bool IsAdmin { get; set; }
    }
}