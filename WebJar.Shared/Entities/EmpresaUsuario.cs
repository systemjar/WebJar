using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJar.Shared.Entities
{
    public class EmpresaUsuario
    {
        public int EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}