﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJar.Shared.Entities
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Navegación a la relación de muchos a muchos
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}