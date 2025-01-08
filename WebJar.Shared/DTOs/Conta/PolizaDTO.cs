using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Entities;

namespace WebJar.Shared.DTOs.Conta
{
    public class PolizaDTO
    {
        public PolizaDTO()
        {
            Detalles = new List<DetalleDTO>();
        }

        public int Id { get; set; }
        public int EmpresaId { get; set; }

        [Display(Name = "Número de documento")]
        [MaxLength(15, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Documento { get; set; }

        public int TipoId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Fecha { get; set; }

        [Display(Name = "A nombre de")]
        [MaxLength(65, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        public string Aquien { get; set; }

        [Display(Name = "Por concepto de")]
        [MaxLength(65, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        public string Porque { get; set; }

        [Column(TypeName = "nvarchar(65)")]
        public string Origen { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string Comentario { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Debe")]
        public decimal SumaDebe => Detalles == null || Detalles.Count == 0 ? 0 : Detalles.Sum(sd => sd.Debe);

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Haber")]
        public decimal SumaHaber => Detalles == null || Detalles.Count == 0 ? 0 : Detalles.Sum(sd => sd.Haber);

        public string? Hechopor { get; set; }

        public List<DetalleDTO>? Detalles { get; set; }
    }
}