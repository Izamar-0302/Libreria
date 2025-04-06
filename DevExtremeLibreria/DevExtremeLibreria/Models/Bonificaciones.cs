using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExtremeLibreria.Models
{
    public class Bonificaciones
    {
        public int Id { get; set; }
        public Empleado Empleado { get; set; }
        public string Tipobonificacion { get; set; }
        public double Monto { get; set; }
    }
}