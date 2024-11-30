using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebJar.Shared.Entities
{
    public class Empresa
    {
        public int Id { get; set; }

        [Display(Name = "Nit Empresa")]
        [MaxLength(15, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nit { get; set; } = null!;

        [Display(Name = "Nombre Empresa")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; } = null!;

        public ICollection<Cuenta>? Cuentas { get; set; }
    }
}