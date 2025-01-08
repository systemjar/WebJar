using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebJar.Shared.Entities.Conta
{
    public class Detalle
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }

        public int PolizaId { get; set; }
        public Poliza? Poliza { get; set; }

        public int TipoId { get; set; }
        public TipoConta? Tipo { get; set; }

        [Display(Name = "Codigo contable")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Codigo { get; set; }

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

        [Column(TypeName = "nvarchar(20)")]
        public string Contras { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Factura { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Serie { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Origen { get; set; }
    }
}