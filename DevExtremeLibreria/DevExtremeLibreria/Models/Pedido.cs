using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExtremeLibreria.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int ProveedorId { get; set; }
        public DateTime Fechapedido { get; set; }
        public DateTime Fechaentrega { get; set; }
        public string Estado { get; set; }
    }
}