using System.ComponentModel.DataAnnotations;

namespace WebJar.Shared.Validaciones.Conta
{
    public class DebeHaberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string valor && (valor.Contains('D') || valor.Contains('H')))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("El campo debe contener la letra 'D' o 'H'.");
        }
    }
}