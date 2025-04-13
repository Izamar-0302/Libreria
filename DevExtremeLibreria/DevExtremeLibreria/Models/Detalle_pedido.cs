using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExtremeLibreria.Models
{
    public class Detalle_pedido
    {
        public int Id { get; set; }
        public Pedido pedido { get; set; }
        public int PedidoId { get; set; }
        public Libro Libro { get; set; }
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Subtotal { get; set; }
    }
}