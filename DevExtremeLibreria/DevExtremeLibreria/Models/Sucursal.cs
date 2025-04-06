using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExtremeLibreria.Models
{
    public class Sucursal
    {
        public int SucursalId { get; set; }
        public string Nombresucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Gerente { get; set; }
        public string Horaentrada { get; set; }
        public string Horasalida { get; set; }
    }
}