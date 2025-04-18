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
    public class Detalle_VentaController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Detalle_Venta
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Detalle_Venta</returns>
        /// <response code="200">Devuelve si los valores son encontrados</response>
        /// <response code="404">Devuelve si no son encontrados</response>
        [HttpGet]
        [SwaggerOperation("GetDetalle_Ventas")]
        [Route("api/GetDetalle_Ventas")]
        public IHttpActionResult Get()
        {
            var Detalleventa = db.Detalles_ventas
                .Include(l => l.Venta)
                .Include(l => l.Libro)
                .Select(l => new
                {
                    l.Id,
                    l.VentaId,
                    l.LibroId,
                    l.Cantidad,
                    l.Preciounitario,
                    l.Subtotal,
                    
                })
                .ToList();

            return Ok(Detalleventa);
        }


        // GET: api/Detalle_Venta/5

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <returns>JSON Detalle_Venta</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetDetalle_Venta")]
        [Route("api/GetDetalle_Venta")]
        public IHttpActionResult Get(int id)
        {
            var Detalleventa = db.Detalles_ventas
                .Include(l => l.Venta)
                .Include(l => l.Libro)
                .Select(l => new
                {
                    l.Id,
                    l.VentaId,
                    l.LibroId,
                    l.Cantidad,
                    l.Preciounitario,
                    l.Subtotal,

                })
                .FirstOrDefault();

            if (Detalleventa == null)
                return NotFound();

            return Ok(Detalleventa);
        }

        // POST: api/Detalle_Venta

        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <returns>JSON Detalle_Venta</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostDetalle_Venta")]
        [Route("api/PostDetalle_Venta")]
        public IHttpActionResult Post(Detalle_Venta detalleventa)
        {
            if (detalleventa == null)
                return BadRequest("Detalle inválido.");

            var venta = db.Ventas.Find(detalleventa.VentaId);
            var libro = db.Libros.Find(detalleventa.LibroId);

            if (venta == null)
                return BadRequest("Pedido no encontrado.");

            if (libro == null)
                return BadRequest("libro no encontrada.");

            detalleventa.Subtotal = detalleventa.Cantidad * detalleventa.Preciounitario;
            db.Detalles_ventas.Add(detalleventa);
            db.SaveChanges();

            return Ok(detalleventa);
        }

        // PUT: api/Detalle_Venta/5

        /// <summary>
        /// modificar valores
        /// </summary>
        /// <returns>JSON Detalle_Venta</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutDetalle_Venta")]
        [Route("api/PutDetalle_Venta")]
        public IHttpActionResult Put(int id,Detalle_Venta detalleventamodificar)
        {
            var detalleventa = db.Detalles_ventas.Find(id);
            if (detalleventa == null)
                return NotFound();


            var ventaNuevo = db.Ventas.Find(detalleventamodificar.VentaId);
            if (ventaNuevo == null)
                return BadRequest("VentaId proporcionado no existe.");


            var libroNuevo = db.Libros.Find(detalleventamodificar.LibroId);
            if (libroNuevo == null)
                return BadRequest("LibroId proporcionado no existe.");


            detalleventa.VentaId = detalleventamodificar.VentaId;
            detalleventa.LibroId = detalleventamodificar.LibroId;
            detalleventa.Cantidad = detalleventamodificar.Cantidad;
            detalleventa.Preciounitario = detalleventamodificar.Preciounitario;
            detalleventa.Subtotal = detalleventa.Cantidad * detalleventa.Preciounitario;

            db.SaveChanges();

            return Ok(detalleventa);
        }

        // DELETE: api/Detalle_Venta/5

        /// <summary>
        /// Eliminar valores
        /// </summary>
        /// <returns>JSON Detalle_Venta</returns>
        /// <response code="200">Devuelve si los valores son eliminados</response>
        /// <response code="404">Devuelve si no son eliminados</response>
        [HttpDelete]
        [SwaggerOperation("DeleteDetalle_Venta")]
        [Route("api/DeleteDetalle_Venta")]
        public IHttpActionResult Delete(int id)
        {
            Detalle_Venta detalleventa = db.Detalles_ventas.Find(id);
            db.Detalles_ventas.Remove(detalleventa);
            db.SaveChanges();
            return Ok(detalleventa);

        }
    }
}
