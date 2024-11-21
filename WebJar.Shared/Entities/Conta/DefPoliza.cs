using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJar.Shared.Entities.Conta
{
    public class DefPoliza
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public Empresa? Empresa { get; set; } = null!;

        public int CuentaID { get; set; }

        public Cuenta? Cuenta { get; set; } = null!;

        [Display(Name = "Codigo contable")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Codigo { get; set; } = null!;

        [Display(Name = "Nombre de la cuenta")]
        [MaxLength(65, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; } = null!;
    }
}