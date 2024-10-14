﻿using System;
using System.Collections.Generic;

namespace SistemaVenta.Model
{
    public partial class Rol
    {
        public Rol()
        {
            MenuRols = new HashSet<MenuRol>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRol { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<MenuRol> MenuRols { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
