﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Venta
    {
        public int VentaId { get; set; }
        public DateTime Fechadeventa { get; set; }
        public virtual Lector Lector { get; set; }
        public int LectorId { get; set; }
        public virtual Empleado Empleado { get; set; }
        public int EmpleadoId { get; set; }
        public virtual Pedido Pedido { get; set; }
        public int? PedidoId { get; set; }
        public virtual Libro Libro { get; set; }
        public int? LibroId { get; set; }
        public virtual MetodoPago MetodoPago { get; set; }
        public int MetodopagoId { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public int SucursalId { get; set; }
        public string Descripcion { get; set; }

        public int Cantidadventa { get; set; }
        public double Montototal { get; set; }
        public double Descuento { get; set; }
        public double Impuesto { get; set; }
        public Venta() { }
    }
}