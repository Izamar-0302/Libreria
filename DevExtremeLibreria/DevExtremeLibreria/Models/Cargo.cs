using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExtremeLibreria.Models
{
    public class Cargo
    {
        public int CargoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Departamento { get; set; }
    }
}