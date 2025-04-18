using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class LibroDto
    {
        public class LibroDTO
        {
            public int LibroId { get; set; }
            public string Titulo { get; set; }
            public int AutorId { get; set; }
            public int EditorialId { get; set; }
            public string Aniopublicacion { get; set; }
            public double Precio { get; set; }
            public string Genero { get; set; }
            public int Cantidad { get; set; }
            public int UbicacionId { get; set; }

            
            public string DescripcionUbicacion { get; set; }

            public int ProveedorId { get; set; }
        }
    }
}