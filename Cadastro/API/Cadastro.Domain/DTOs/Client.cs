using System;
using System.ComponentModel.DataAnnotations;
using Cadastro.Domain.Abstraction.DTOs;
using Cadastro.Domain.Validation;

namespace Cadastro.Domain.DTOs
{
    public class Client : IDTO
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Nome obrigatório")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo CPF obrigatório")]
        [ValidateCPF(ErrorMessage = "CPF inválido")]
        public string CPF { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Ocupação obrigatório")]
        public string Occupation { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Data de Nascimento obrigatório")]
        [ValidateBirthDate(ErrorMessage = "Data de nascimento inválida")]
        public DateTime DateOfBirth { get; set; }

        [ValidateComplexType]
        public Address Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Telefone obrigatório")]
        public string PhoneNumber { get; set; }
    }
}