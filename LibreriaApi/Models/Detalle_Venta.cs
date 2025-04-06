using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Detalle_Venta
    {
        public int ID { get; set; }
        public Venta Venta { get; set; }
        public Libro Libro { get; set; }
        public int Cantidad { get; set; }
        public double Preciounitario { get; set; }
        public double Subtotal { get; set; }

        public Detalle_Venta() { }
    }
}