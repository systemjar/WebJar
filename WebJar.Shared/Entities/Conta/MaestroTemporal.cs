using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJar.Shared.Entities.Conta
{
    public class MaestroTemporal
    {
        public int Id { get; set; }

        [Display(Name = "Número de documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Documento { get; set; }

        public string Tipo { get; set; }
        public string Fecha { get; set; }
        public string Aquien { get; set; }
        public string Porque { get; set; }
        public string Origen { get; set; }
        public string Comentario { get; set; }

        public string ElMes;

        public string FechaOperado { get; set; }
    }
}