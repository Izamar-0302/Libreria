using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Empleado_planilla
    {
        public int Id { get; set; }
        public Empleado Empleado { get; set; }
        public Bonificaciones Bono { get; set; }
        public double Anticipo { get; set; }
        public Deducciones Deduccion { get; set; }
        public double Sueldoneto { get; set; }
        public Empleado_planilla() { }
    }
}