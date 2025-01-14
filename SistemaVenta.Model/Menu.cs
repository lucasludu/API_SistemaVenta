﻿using System;
using System.Collections.Generic;

namespace SistemaVenta.Model
{
    public partial class Menu
    {
        public Menu()
        {
            MenuRols = new HashSet<MenuRol>();
        }

        public int IdMenu { get; set; }
        public string? Nombre { get; set; }
        public string? Icono { get; set; }
        public string? Url { get; set; }

        public virtual ICollection<MenuRol> MenuRols { get; set; }
    }
}
