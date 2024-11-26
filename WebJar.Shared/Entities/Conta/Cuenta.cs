using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebJar.Shared.Validaciones.Conta;

namespace WebJar.Shared.Entities.Conta
{
    public class Cuenta
    {
        public int Id { get; set; }

        [Display(Name = "Codigo contable")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Codigo { get; set; } = null!;

        [Display(Name = "Nombre de la cuenta")]
        [MaxLength(65, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Tipo de saldo")]
        [MaxLength(1, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DebeHaber(ErrorMessage = "El campo DebeHaber debe contener la letra 'D' o 'H'.")]
        public string DebeHaber { get; set; } = null!;

        [Column(TypeName = "decimal(13,2)")]
        public decimal? Saldo { get; set; }

        [Column(TypeName = "decimal(13,2)")]
        public decimal? Cargos { get; set; }

        [Column(TypeName = "decimal(13,2)")]
        public decimal? Abonos { get; set; }

        [Column(TypeName = "decimal(13,2)")]
        public decimal? SaldoAcumulado => DebeHaber == "D" ? (Saldo + Cargos - Abonos) : (Saldo - Cargos + Abonos);

        [Column(TypeName = "decimal(13,2)")]
        public decimal? SaldoMes { get; set; }

        [Column(TypeName = "decimal(13,2)")]
        public decimal? CargosMes { get; set; }

        [Column(TypeName = "decimal(13,2)")]
        public decimal? AbonosMes { get; set; }

        [Column(TypeName = "decimal(13,2)")]
        public decimal? SaldoFinMes
        {
            get
            {
                if (DebeHaber == "D")
                    return (SaldoMes + CargosMes - AbonosMes);
                else
                    return (SaldoMes - CargosMes + AbonosMes);
            }
        }

        [Column(TypeName = "decimal(13,2)")]
        public decimal? SaldoCierre { get; set; }

        [Display(Name = "Codigo Cuenta Mayor")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        public string? CodigoMayor { get; set; }

        [Display(Name = "Codigo Presupuesto")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        public string? CodigoPres { get; set; }

        public string? IngresoCash { get; set; }

        public string? EgresoCash { get; set; }

        public string? Nit { get; set; }
    }
}