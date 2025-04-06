using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Planilla
    {
        public int Id { get; set; }

        public DateTime Fechapago { get; set; }
        public Planilla() { }
    }
}