using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Bonificaciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BonificacionesId { get; set; }
        public Empleado Empleado { get; set; }
        public int EmpleadoId { get; set; }
        public string Tipobonificacion { get; set; }
        public double Monto { get; set; }
        public Bonificaciones() { }
    }
}