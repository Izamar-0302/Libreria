using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Detalle_Venta
    {
        public int Id { get; set; }
        public virtual Venta Venta { get; set; }
        public int VentaId { get; set; }
        public virtual Libro Libro { get; set; }
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
        public double Preciounitario { get; set; }
        public double Subtotal { get; set; }

        public Detalle_Venta() { }
    }
}