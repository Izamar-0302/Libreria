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
        public IHttpActionResult Get()
        {
             var libros = db.Libros
        .Include(l => l.Autor)
        .Include(l => l.Editorial)
        .Include(l => l.Ubicacion)
        .Include(l => l.Proveedor)
        .Select(l => new
        {
            l.LibroId,
            l.Titulo,
            l.AutorId,
            l.EditorialId,
            l.Aniopublicacion,
            l.Precio,
            l.Genero,
            l.Cantidad,
            l.UbicacionId,
            l.ProveedorId
        })
        .ToList();

    return Ok(libros);
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
            var libro = db.Libros
                .Include(l => l.Autor)
                .Include(l => l.Editorial)
                .Include(l => l.Ubicacion)
                .Include(l => l.Proveedor)
                .Where(l => l.LibroId == id)
                .Select(l => new
                {
                    l.LibroId,
                    l.Titulo,
                    l.AutorId,
                    l.EditorialId,
                    l.Aniopublicacion,
                    l.Precio,
                    l.Genero,
                    l.Cantidad,
                    l.UbicacionId,
                    l.ProveedorId
                })
                .FirstOrDefault();

            if (libro == null)
                return NotFound();

            return Ok(libro);
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
            if (libro == null)
                return BadRequest("Libro inválido.");

            var autor = db.Autores.Find(libro.AutorId);
            var editorial = db.Editoriales.Find(libro.EditorialId);
            var ubicacion = db.Ubicaciones.Find(libro.UbicacionId);
            var proveedor = db.Proveedores.Find(libro.ProveedorId);
            if (autor == null)
                return BadRequest("Autor no encontrado.");

            if (editorial == null)
                return BadRequest("Editorial no encontrada.");
            if (ubicacion == null)
                return BadRequest("Ubicacion no encontrada.");

            if (proveedor == null)
                return BadRequest("Proveedor no encontrado.");
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
        public IHttpActionResult Put(int id, Libro libroActualizado)
        {
            var libro = db.Libros.Find(id);
            if (libro == null)
                return NotFound();

            var autor = db.Autores.Find(libro.AutorId);
            var editorial = db.Editoriales.Find(libro.EditorialId);
            var ubicacion = db.Ubicaciones.Find(libro.UbicacionId);
            var proveedor = db.Proveedores.Find(libro.ProveedorId);

            if (autor == null)
                return BadRequest("Autor no encontrado.");

            if (editorial == null)
                return BadRequest("Editorial no encontrada.");
            if (ubicacion == null)
                return BadRequest("Ubicacion no encontrada.");

            if (proveedor == null)
                return BadRequest("Proveedor no encontrado.");

            libro.Titulo = libroActualizado.Titulo;
            libro.AutorId = libroActualizado.AutorId;
            libro.EditorialId = libroActualizado.EditorialId;
            libro.Aniopublicacion = libroActualizado.Aniopublicacion;
            libro.Precio = libroActualizado.Precio;
            libro.Genero = libroActualizado.Genero;
            libro.Cantidad = libroActualizado.Cantidad;
            libro.UbicacionId = libroActualizado.UbicacionId;
            libro.ProveedorId = libroActualizado.ProveedorId;
            db.SaveChanges();
            return Ok(libro);
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
