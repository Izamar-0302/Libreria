using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Empleado:Persona
    {

        public int EmpleadoId { get; set; }
        public virtual Cargo Cargo { get; set; }
        public int CargoId { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public int SucursalId { get; set; }

        public double Salario { get; set; }
        public Empleado() { }
    }
}