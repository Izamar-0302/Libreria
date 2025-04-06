using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Editorial
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public string Fundacion { get; set; }
        public string Catalogo { get; set; }
        public Editorial() { }
    }
}