using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevExtremeLibreria.Models
{
    public class Sucursal
    {
        [Required(ErrorMessage = "Id es requerido")]
        public int SucursalId { get; set; }
        [Display(Name = "Nombre sucursal")]
        public string Nombresucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Gerente { get; set; }
        [Display(Name = "Hora entrada")]
        public string Horaentrada { get; set; }
        [Display(Name = "Hora salida")]
        public string Horasalida { get; set; }
    }
}