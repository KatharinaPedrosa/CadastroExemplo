using System;
using System.ComponentModel.DataAnnotations;
using Cadastro.Domain.Abstraction.DTOs;

namespace Cadastro.Domain.DTOs
{
    public class Client : IDTO
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [MinLength(11, ErrorMessage = "CPF deve conter 11 dígitos")]
        [MaxLength(11, ErrorMessage = "CPF deve conter 11 dígitos")]
        public string CPF { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string Occupation { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public DateTime DateOfBirth { get; set; }

        public Address Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string PhoneNumber { get; set; }
    }
}