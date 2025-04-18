using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Deducciones
    {
        public int DeduccionesId { get; set; }
        public virtual Empleado Empleado { get; set; }
        public int EmpleadoId { get; set; }
        public string TipoDeduccion { get; set; }
        public double Monto { get; set; }
        public Deducciones() { }
    }
}