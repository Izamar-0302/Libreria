using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class MetodoPago
    {
        public int MetodopagoId { get; set; }
        public string Metodo { get; set; }
        public DateTime Fechapago { get; set; }
        public MetodoPago() { }
    }
}