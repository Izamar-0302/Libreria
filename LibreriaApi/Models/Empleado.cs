using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Empleado:Persona
    {
        [Key]
        public int EmpleadoId { get; set; }
        public Cargo Cargo { get; set; }
        public int CargoId { get; set; }
        public Sucursal Sucursal { get; set; }
        public int SucursalId { get; set; }

        public double Salario { get; set; }
        public Empleado() { }
    }
}