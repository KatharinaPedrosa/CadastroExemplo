using System.ComponentModel.DataAnnotations;
using Cadastro.Domain.Abstraction.DTOs;

namespace Cadastro.Domain.DTOs
{
    public class Address : IDTO
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo logradouro obrigatório")]
        public string Street { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo bairro obrigatório")]
        public string Neighborhood { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo cidade obrigatório")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo estado obrigatório")]
        public string State { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo país obrigatório")]
        public string Country { get; set; }
    }
}