using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebJar.Shared.Entities.Conta;

namespace WebJar.Shared.DTOs.Conta
{
    public class DetalleDTO
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public int DocumentoId { get; set; }

        public string? Codigo { get; set; }

        [Display(Name = "Codigo contable")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CuentaId { get; set; }

        public Cuenta? Cuenta { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Debe")]
        public decimal Debe { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Haber")]
        public decimal Haber { get; set; }

        public string Origen { get; set; }
    }
}