using System.ComponentModel.DataAnnotations;
using Cadastro.Domain.Abstraction.DTOs;

namespace Cadastro.Domain.DTOs
{
    public class Address : IDTO
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string Street { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string Neighborhood { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string State { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string Country { get; set; }
    }
}