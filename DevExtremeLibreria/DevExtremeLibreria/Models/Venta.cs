using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExtremeLibreria.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fechadeventa { get; set; }
        public Lector Lector { get; set; }
        public Empleado Empleado { get; set; }
        public Pedido Pedido { get; set; }
        public Libro Libro { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public Sucursal Sucursal { get; set; }
        public string Descripcion { get; set; }

        public int Cantidadventa { get; set; }
        public double Montototal { get; set; }
        public double Descuento { get; set; }
        public double Impuesto { get; set; }
    }
}