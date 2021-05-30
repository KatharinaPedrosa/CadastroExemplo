using System.ComponentModel.DataAnnotations;
using Cadastro.Domain.Abstraction.DTOs;

namespace Cadastro.Domain.DTOs
{
    public class User : IDTO
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo login obrigatório")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo senha obrigatório")]
        public string PasswordHash { get; set; }

        public string NewPasswordHash { get; set; }

        public string Token { get; set; }

        public bool IsAdmin { get; set; }
    }
}