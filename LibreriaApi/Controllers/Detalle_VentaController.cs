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
            var query = from venta1 in db.Ventas
                        join detalleVenta in db.Detalles_ventas on venta1.VentaId equals detalleVenta.Venta.VentaId
                        join libro1 in db.Libros on detalleVenta.Libro.LibroId equals libro1.LibroId
                        select new
                        {
                            IddetalleVenta = detalleVenta.Id,
                            IdVenta = venta1.VentaId,
                            preciounitario = detalleVenta.Preciounitario,
                            cantidad = detalleVenta.Cantidad,
                            subtotal = detalleVenta.Subtotal,
                            Idlibro = libro1.LibroId
                        };

            return Ok(query);
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
            var query = from venta1 in db.Ventas
                        join detalleVenta in db.Detalles_ventas on venta1.VentaId equals detalleVenta.Venta.VentaId
                        join libro1 in db.Libros on detalleVenta.Libro.LibroId equals libro1.LibroId
                        where detalleVenta.Id== id
                        select new
                        {
                            IddetalleVenta = detalleVenta.Id,
                            IdVenta = venta1.VentaId,
                            preciounitario = detalleVenta.Preciounitario,
                            cantidad = detalleVenta.Cantidad,
                            subtotal = detalleVenta.Subtotal,
                            Idlibro = libro1.LibroId
                        };

            return Ok(query);
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
        public IHttpActionResult Post(Detalle_Venta detalleventa, int idventa, int idlibro)
        {
            Libro libroexistente = db.Libros.Find(idlibro);
            if (libroexistente == null)
            {
                return NotFound();
            }
            detalleventa.Libro = libroexistente;
            Venta ventaexistente = db.Ventas.Find(idventa);
            if (ventaexistente == null)
            {
                return NotFound();
            }
            detalleventa.Venta = ventaexistente;
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
        public IHttpActionResult Put(Detalle_Venta detalleventamodificar, int idventa, int idlibro)
        {
            Libro libroexistente = db.Libros.Find(idlibro);
            if (libroexistente == null)
            {
                return NotFound();
            }
            detalleventamodificar.Libro = libroexistente;
            Venta ventaexistente = db.Ventas.Find(idventa);
            if (ventaexistente == null)
            {
                return NotFound();
            }
            detalleventamodificar.Venta = ventaexistente;
            int id = detalleventamodificar.Id;
            db.Entry(detalleventamodificar).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(detalleventamodificar);
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
