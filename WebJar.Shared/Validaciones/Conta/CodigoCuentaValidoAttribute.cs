using System.ComponentModel.DataAnnotations;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Servicios;

namespace WebJar.Shared.Validaciones.Conta
{
    public class CodigoCuentaValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cuenta = (Cuenta)validationContext.ObjectInstance;
            var empresaService = validationContext.GetService(typeof(EmpresaService)) as EmpresaService;

            // Si la empresa es null, no realizamos validación y devolvemos éxito
            if (empresaService?.EmpresaSeleccionada == null)
            {
                return ValidationResult.Success;
            }

            var empresa = empresaService.EmpresaSeleccionada;
            var codigo = cuenta.Codigo;
            var niveles = new[] { empresa.Nivel1, empresa.Nivel2, empresa.Nivel3, empresa.Nivel4, empresa.Nivel5, empresa.Nivel6 };
            // Convertir los niveles a enteros
            int[] nivelesInt = new int[niveles.Length];
            for (int i = 0; i < niveles.Length; i++)
            {
                if (!int.TryParse(niveles[i], out nivelesInt[i]))
                {
                    return new ValidationResult($"El nivel {i + 1} no es un valor válido.");
                }
            }
            // Calcular las longitudes válidas de los códigos
            int sumaLongitud = 0;
            var longitudesValidas = nivelesInt
                .Where(n => n > 0)
                .Select(n =>
                {
                    sumaLongitud += n;
                    return sumaLongitud;
                })
                .ToArray();
            // Verificar si la longitud del código es válida
            if (Array.Exists(longitudesValidas, longitud => longitud == codigo.Length))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"El Código {codigo} no tiene una longitud válida.");
            }
        }
    }
}