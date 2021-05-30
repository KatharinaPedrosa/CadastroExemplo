using System.ComponentModel.DataAnnotations;
using Cadastro.Domain.Helpers;

namespace Cadastro.Domain.Validation
{
    public class ValidateCPF : ValidationAttribute
    {
        protected override ValidationResult IsValid
                  (object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return new ValidationResult("Campo CPF obrigatório", new[] { validationContext.MemberName });
            var cpf = value.ToString();

            return ValidationHelper.IsCpf(cpf)
                ? null
                : new ValidationResult("CPF inválido", new[] { validationContext.MemberName });
        }
    }
}