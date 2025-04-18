using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static System.Data.Entity.Migrations.Model.UpdateDatabaseOperation;

namespace LibreriaApi.Models
{
    public class Empleado_planilla
    {
        [Key]
        public int Id { get; set; }
        public virtual Empleado Empleado { get; set; }
        public int EmpleadoId { get; set; }
        public virtual Bonificaciones Bono { get; set; }
        public int BonificacionesId { get; set; }
        public double Anticipo { get; set; }
        public virtual Deducciones Deduccion { get; set; }
        public int DeduccionesId { get; set; }
        public double Sueldoneto { get; set; }
        public Empleado_planilla() { }
    }
}