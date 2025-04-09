using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Lector : Persona
    {
        public int LectorId { get; set; }
        public Lector() { }
    }
}