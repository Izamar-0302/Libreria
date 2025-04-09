using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class Libro
    {
        public int LibroId { get; set; }
        public string Titulo { get; set; }

        public Autor Autor { get; set; }
        public int AutorId { get; set; }

        public Editorial Editorial { get; set; }
        public int EditorialId { get; set; }
        public string Aniopublicacion { get; set; }
        public double Precio { get; set; }
        public string Genero { get; set; }
        public int Cantidad { get; set; }

        public Ubicacion Ubicacion { get; set; }
        public int UbicacionId { get; set; }

        public Proveedor Proveedor { get; set; }
        public int ProveedorId { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }
        public Libro() { }
    }
}