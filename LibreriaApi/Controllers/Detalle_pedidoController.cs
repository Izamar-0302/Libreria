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
    public class Detalle_pedidoController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Detalle_pedido
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Detalle_pedido</returns>
        /// <response code="200">Devuelve si los valores son encontrados</response>
        /// <response code="404">Devuelve si no son encontrados</response>
        [HttpGet]
        [SwaggerOperation("GetDetalle_pedidos")]
        [Route("api/GetDetalle_pedidos")]
        public IHttpActionResult Get()
        {
            var Detallepedido = db.Detalles_pedidos
       .Include(l => l.Pedido)
       .Include(l => l.Libro)
       
       .Select(l => new
       {
           l.Id,
           l.PedidoId,
           l.LibroId,
           l.Cantidad,
           l.PrecioUnitario,
           l.Subtotal,

       })
       .ToList();

            return Ok(Detallepedido);
        }


        // GET: api/Detalle_pedido/5

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <returns>JSON Detalle_pedido</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetDetalle_pedido")]
        [Route("api/GetDetalle_pedido")]
        public IHttpActionResult Get(int id)
        {
                var Detallepedido = db.Detalles_pedidos
                .Include(l => l.Pedido)
                .Include(l => l.Libro)

                .Select(l => new
                {
                    l.Id,
                    l.PedidoId,
                    l.LibroId,
                    l.Cantidad,
                    l.PrecioUnitario,
                    l.Subtotal,
                })
                .FirstOrDefault();

            if (Detallepedido == null)
                return NotFound();

            return Ok(Detallepedido);
        }

        // POST: api/Detalle_pedido

        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <returns>JSON Detalle_pedido</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostDetalle_pedido")]
        [Route("api/PostDetalle_pedido")]
        public IHttpActionResult Post(Detalle_pedido detalle_Pedido)
        {
            if (detalle_Pedido == null)
                return BadRequest("Libro inválido.");

            var pedido = db.Pedidos.Find(detalle_Pedido.PedidoId);
            var libro = db.Libros.Find(detalle_Pedido.LibroId);
           
            if (pedido == null)
                return BadRequest("Pedido no encontrado.");

            if (libro == null)
                return BadRequest("libro no encontrada.");

            detalle_Pedido.Subtotal = detalle_Pedido.Cantidad * detalle_Pedido.PrecioUnitario;
            db.Detalles_pedidos.Add(detalle_Pedido);
            db.SaveChanges();

            return Ok(detalle_Pedido);
        }

        // PUT: api/Detalle_pedido/5

        /// <summary>
        /// modificar valores
        /// </summary>
        /// <returns>JSON Detalle_pedido</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutDetalle_pedido")]
        [Route("api/PutDetalle_pedido")]
        public IHttpActionResult Put(int id, Detalle_pedido detalleActualizado)
        {
            var detallepedido = db.Detalles_pedidos.Find(id);
            if (detallepedido == null)
                return NotFound();

           
            var pedidoNuevo = db.Pedidos.Find(detalleActualizado.PedidoId);
            if (pedidoNuevo == null)
                return BadRequest("PedidoId proporcionado no existe.");

         
            var libroNuevo = db.Libros.Find(detalleActualizado.LibroId);
            if (libroNuevo == null)
                return BadRequest("LibroId proporcionado no existe.");

         
            detallepedido.PedidoId = detalleActualizado.PedidoId;
            detallepedido.LibroId = detalleActualizado.LibroId;
            detallepedido.Cantidad = detalleActualizado.Cantidad;
            detallepedido.PrecioUnitario = detalleActualizado.PrecioUnitario;
            detallepedido.Subtotal = detallepedido.Cantidad * detallepedido.PrecioUnitario;

            db.SaveChanges();

            return Ok(detallepedido);
        }

        // DELETE: api/Detalle_pedido/5

        /// <summary>
        /// Eliminar valores
        /// </summary>
        /// <returns>JSON Detalle_pedido</returns>
        /// <response code="200">Devuelve si los valores son eliminados</response>
        /// <response code="404">Devuelve si no son eliminados</response>
        [HttpDelete]
        [SwaggerOperation("DeleteDetalle_pedido")]
        [Route("api/DeleteDetalle_pedido")]
        public IHttpActionResult Delete(int id)
        {
            Detalle_pedido detallepedido = db.Detalles_pedidos.Find(id);
            db.Detalles_pedidos.Remove(detallepedido);
            db.SaveChanges();
            return Ok(detallepedido);

        }
    }
}
