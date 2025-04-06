using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime Fechapedido { get; set; }
        public DateTime Fechaentrega { get; set; }
        public string Estado { get; set; }
        public Pedido() { }
    }
}