using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExtremeLibreria.Models
{
    public class Empleado:Persona
    {
        public int EmpleadoId { get; set; }
        public Cargo Cargo { get; set; }
        public int CargoId { get; set; }
        public Sucursal Sucursal { get; set; }
        public int SucursalId { get; set; }

        public double Salario { get; set; }
        public Empleado() { }
    }
}