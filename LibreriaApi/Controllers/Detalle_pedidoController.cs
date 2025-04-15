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
        public IEnumerable<Detalle_pedido> Get()
        {
            return db.Detalles_pedidos;
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
            var query = from pedido1 in db.Pedidos
                        join detallepedido in db.Detalles_pedidos on pedido1.PedidoId equals detallepedido.pedido.PedidoId
                        join libro1 in db.Libros on detallepedido.Libro.LibroId equals libro1.LibroId
                        where detallepedido.Id == id
                        select new
                        {
                            IdDetallepedido = detallepedido.Id,
                            Pedido = detallepedido.pedido,
                            libro = detallepedido.Libro,
                            cantidad = detallepedido.Cantidad,
                            detallepedido.PrecioUnitario,
                            detallepedido.Subtotal
                        };

            return Ok(query);
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
        public IHttpActionResult Post(Detalle_pedido detallepedido)
        {
            if (detallepedido.Libro != null)
            {
                Libro libro = db.Libros.Find(detallepedido.Libro.LibroId);
                detallepedido.Libro = libro;
            }
            if (detallepedido.pedido != null)
            {
                Pedido pedido = db.Pedidos.Find(detallepedido.pedido.PedidoId);
                detallepedido.pedido = pedido;
            }

            
            db.Detalles_pedidos.Add(detallepedido);
            db.SaveChanges();
            return Ok(detallepedido);
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
        public IHttpActionResult Put(Detalle_pedido detallepedidomodificado, int idlibro, int idpedido)
        {
            Libro libroexistente = db.Libros.Find(idlibro);
            if (libroexistente == null)
            {
                return NotFound();
            }
            detallepedidomodificado.Libro = libroexistente;
            Pedido pedidoexistente = db.Pedidos.Find(idpedido);
            if (pedidoexistente == null)
            {
                return NotFound();
            }
            detallepedidomodificado.pedido = pedidoexistente;
            int id = detallepedidomodificado.Id;
            db.Entry(detallepedidomodificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(detallepedidomodificado);
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
