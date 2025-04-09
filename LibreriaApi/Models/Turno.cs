using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Turno
    {
        public int TurnoId { get; set; }
        public Empleado Empleado { get; set; }
        public int EmpleadoId { get; set; }
        public string Horainicio { get; set; }
        public string Horafinal { get; set; }
        public Turno() { }
    }
}