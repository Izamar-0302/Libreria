using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Empleado:Persona
    {
        public int Id { get; set; }
        public Cargo Cargo { get; set; }
        public Sucursal Sucursal { get; set; }

        public double Salario { get; set; }
        public Empleado() { }
    }
}