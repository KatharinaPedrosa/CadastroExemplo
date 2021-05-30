using System;
using System.ComponentModel.DataAnnotations;

namespace Cadastro.Domain.Validation
{
    public class ValidateBirthDate : ValidationAttribute
    {
        protected override ValidationResult IsValid
                  (object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            if (DateTime.TryParse(value.ToString(), out var birthDate))
            {
                var now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                return birthDate < now && birthDate > now.AddYears(-200)
                    ? null
                    : new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }
            return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
        }
    }
}