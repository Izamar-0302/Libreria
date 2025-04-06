using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Autor
    {
        public int AutorId { get; set; }
        public string Nombre { get; set; }
        public string Nacionalidad { get; set; }
        public DateTime Fechadenacimiento { get; set; }
        public string Biografia { get; set; }
        public Autor() { }
    }
}