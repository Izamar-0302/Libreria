using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExtremeLibreria.Models
{
    public class Bonificaciones
    {
        public int BonificacionesId { get; set; }
        public Empleado Empleado { get; set; }
        public int EmpleadoId { get; set; }
        public string Tipobonificacion { get; set; }
        public double Monto { get; set; }
    }
}