using LibreriaApi.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LibreriaApi.Controllers
{
    public class LibroController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Libro
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Libro</returns>
        /// <response code="200">Devuelve si los valores son encontrados</response>
        /// <response code="404">Devuelve si no son encontrados</response>
        [HttpGet]
        [SwaggerOperation("GetLibros")]
        [Route("api/GetLibros")]
        public IEnumerable<Libro> Get()
        {
            return db.Libros;
        }


        // GET: api/Libro/5

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <returns>JSON Libro</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetLibro")]
        [Route("api/GetLibro")]
        public IHttpActionResult Get(int id)
        {
            var query = from autor1 in db.Autores
                        join libro in db.Libros on autor1.AutorId equals libro.Autor.AutorId
                        join editorial1 in db.Editoriales on libro.Editorial.EditorialId equals editorial1.EditorialId
                        join ubicacion1 in db.Ubicaciones on libro.Ubicacion.UbicacionId equals ubicacion1.UbicacionId
                        join proveedor1 in db.Proveedores on libro.Proveedor.ProveedorId equals proveedor1.ProveedorId
                        where libro.LibroId == id
                        select new
                        {
                            IdLibro = libro.LibroId,
                            TituloLibro = libro.Titulo,
                            IdAutor = autor1.AutorId,
                            Ideditorial = editorial1.EditorialId,
                            Aniodepublicacion = libro.Aniopublicacion,
                            cantidad = libro.Cantidad,
                            precio = libro.Precio,
                            ubicacion = ubicacion1.UbicacionId,
                            proveedor = proveedor1.ProveedorId
                        };

            return Ok(query);
        }

        // POST: api/Libro

        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <returns>JSON Libro</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostLibro")]
        [Route("api/PostLibro")]
        public IHttpActionResult Post(Libro libro)
        {
            if (libro.Autor != null)
            {
                Autor autorexistente = db.Autores.Find(libro.Autor.AutorId);
                libro.Autor = autorexistente;

            }


            if (libro.Editorial != null)
            {
                Editorial editoexistente = db.Editoriales.Find(libro.Editorial.EditorialId);
                libro.Editorial = editoexistente;
            }


            if (libro.Ubicacion != null)
            {
                Ubicacion ubiexistente = db.Ubicaciones.Find(libro.Ubicacion.UbicacionId);
                libro.Ubicacion = ubiexistente;
            }


            if (libro.Proveedor != null)
            {
                Proveedor proveedorexistente = db.Proveedores.Find(libro.Proveedor.ProveedorId);
                libro.Proveedor = proveedorexistente;
            }
            db.Libros.Add(libro);
            db.SaveChanges();
            return Ok(libro);
        }

        // PUT: api/Libro/5

        /// <summary>
        /// modificar valores
        /// </summary>
        /// <returns>JSON Libro</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutLibro")]
        [Route("api/PutLibro")]
        public IHttpActionResult Put(Libro libromodificar)
        {
            
            if (libromodificar.Autor != null)
            {
                Autor autorexistente = db.Autores.Find(libromodificar.Autor.AutorId);
                libromodificar.Autor = autorexistente;

            }
            
            
            if (libromodificar.Editorial != null)
            {
                Editorial editoexistente = db.Editoriales.Find(libromodificar.Editorial.EditorialId);
                libromodificar.Editorial = editoexistente;
            }
            
            
            if (libromodificar.Ubicacion != null)
            {
                Ubicacion ubiexistente = db.Ubicaciones.Find(libromodificar.Ubicacion.UbicacionId);
                libromodificar.Ubicacion = ubiexistente;
            }
            
            
            if (libromodificar.Proveedor!= null)
            {
                Proveedor proveedorexistente = db.Proveedores.Find(libromodificar.Proveedor.ProveedorId);
                libromodificar.Proveedor = proveedorexistente;
            }
            
            int id = libromodificar.LibroId;
            db.Entry(libromodificar).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(libromodificar);
        }

        // DELETE: api/Libro/5

        /// <summary>
        /// Eliminar valores
        /// </summary>
        /// <returns>JSON Libro</returns>
        /// <response code="200">Devuelve si los valores son eliminados</response>
        /// <response code="404">Devuelve si no son eliminados</response>
        [HttpDelete]
        [SwaggerOperation("DeleteLibro")]
        [Route("api/DeleteLibro")]
        public IHttpActionResult Delete(int id)
        {
            Libro libro = db.Libros.Find(id);
            db.Libros.Remove(libro);
            db.SaveChanges();
            return Ok(libro);

        }
    }
}
