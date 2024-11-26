using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebJar.Shared.Entities.Conta;

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
        [MaxLength(15, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Dirección Empresa")]
        [MaxLength(65, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Direccion { get; set; } = null!;

        [Display(Name = "Patrono")]
        [MaxLength(65, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Patrono { get; set; } = null!;

        [Display(Name = "Dirección Patrono")]
        [MaxLength(65, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string DirecionPatrono { get; set; } = null!;

        [Display(Name = "Número Patronal")]
        [MaxLength(15, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string NumeroPatronal { get; set; } = null!;

        [Display(Name = "Proceso de producción")]
        [MaxLength(1, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Produce { get; set; } = null!;

        [Display(Name = "Largo Nivel 1")]
        [MaxLength(1, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Nivel1 { get; set; } = null!;

        [Display(Name = "Largo Nivel 2")]
        [MaxLength(1, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Nivel2 { get; set; } = null!;

        [Display(Name = "Largo Nivel 3")]
        [MaxLength(1, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Nivel3 { get; set; } = null!;

        [Display(Name = "Largo Nivel 4")]
        [MaxLength(1, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Nivel4 { get; set; } = null!;

        [Display(Name = "Largo Nivel 5")]
        [MaxLength(1, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Nivel5 { get; set; } = null!;

        [Display(Name = "Largo Nivel 6")]
        [MaxLength(1, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Nivel6 { get; set; } = null!;

        [Display(Name = "Activo")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Activo { get; set; } = null!;

        [Display(Name = "Pasivo")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Pasivo { get; set; } = null!;

        [Display(Name = "Capital")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Capital { get; set; } = null!;

        [Display(Name = "Ventas")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Ventas { get; set; } = null!;

        [Display(Name = "Costos")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Costos { get; set; } = null!;

        [Display(Name = "Gastos")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Gastos { get; set; } = null!;

        [Display(Name = "Otros Ingresos")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string OtrosIngresos { get; set; } = null!;

        [Display(Name = "OtrosGastos")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string OtrosGastos { get; set; } = null!;

        [Display(Name = "Produccion")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Produccion { get; set; } = null!;

        [Display(Name = "Porcentaje IVA")]
        [Range(0.01, 100.00, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public double PorcentajeIva { get; set; }

        //public ICollection<Cuenta>? Cuentas { get; set; }

        //public ICollection<DefPoliza>? DefPoliza { get; set; }
    }
}