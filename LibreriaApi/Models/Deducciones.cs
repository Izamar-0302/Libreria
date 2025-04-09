using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Deducciones
    {
        public int Id { get; set; }
        public Empleado Empleado { get; set; }
        
        public string TipoDeduccion { get; set; }
        public double Monto { get; set; }
        public Deducciones() { }
    }
}