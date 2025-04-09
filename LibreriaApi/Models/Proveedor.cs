using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Proveedor
    {
        public int ProveedorId { get; set; }
        public string Nombreproveedor { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }

        public string Telefono { get; set; }
        public virtual ICollection<Pedido> Pedidoes { get; set; }
        public Proveedor() { }
    }
}