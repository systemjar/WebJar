using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJar.Shared.DTOs
{
    public class PaginationDTO
    {
        public int Id { get; set; }

        //Numero de pagina
        public int Page { get; set; } = 1;

        //Registros por pagina
        public int RecordsNumber { get; set; } = 10;

        //Filtro de busqueda
        public string? Filter { get; set; }

        //Filtro pro agrupacion
        public string? AgruparFilter { get; set; }
    }
}