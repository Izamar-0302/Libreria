using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Ubicacion
    {
        public int UbicacionId { get; set; }
        public string Numerodeestante { get; set; }
        public string Numerodepasillo { get; set; }
        public Ubicacion() { }
    }
}