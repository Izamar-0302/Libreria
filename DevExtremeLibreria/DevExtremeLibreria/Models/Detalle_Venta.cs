using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExtremeLibreria.Models
{
    public class Detalle_Venta
    {
        public int Id { get; set; }
        public Venta Venta { get; set; }
        public int VentaId { get; set; }
        public Libro Libro { get; set; }
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
        public double Preciounitario { get; set; }
        public double Subtotal { get; set; }
    }
}