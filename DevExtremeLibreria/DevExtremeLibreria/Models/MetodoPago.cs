using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExtremeLibreria.Models
{
    public class MetodoPago
    {
        public int Id { get; set; }
        public string Metodo { get; set; }
        public DateTime Fechapago { get; set; }
    }
}