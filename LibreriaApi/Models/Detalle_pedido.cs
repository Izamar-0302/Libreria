using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Detalle_pedido
    {
      
        public int Id { get; set; }
        
        [JsonIgnore]
        public virtual Pedido Pedido { get; set; }
        public int PedidoId { get; set; }
        [JsonIgnore]
        public virtual Libro Libro { get; set; }
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Subtotal { get; set; }

        public Detalle_pedido() { }
    }
}