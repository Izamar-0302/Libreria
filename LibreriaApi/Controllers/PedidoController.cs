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
    public class PedidoController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Pedido
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Pedido</returns>
        /// <response code="200">Devuelve si los valores son encontrados</response>
        /// <response code="404">Devuelve si no son encontrados</response>
        [HttpGet]
        [SwaggerOperation("GetPedidos")]
        [Route("api/GetPedidos")]
        public IHttpActionResult Get()
        {
            var query = from proveedor1 in db.Proveedores
                        join pedido in db.Pedidos on proveedor1.ProveedorId equals pedido.Proveedor.ProveedorId
                        select new
                        {
                            Idpedido = pedido.PedidoId,
                            fechaentrega = pedido.Fechaentrega,
                            fechapedido = pedido.Fechapedido,
                            estado = pedido.Estado,
                            idproveedor = proveedor1.ProveedorId
                        };

            return Ok(query);
        }


        // GET: api/Pedido/5

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <returns>JSON Pedido</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetPedido")]
        [Route("api/GetPedido")]
        public IHttpActionResult Get(int id)
        {
            var query = from proveedor1 in db.Proveedores
                        join pedido in db.Pedidos on proveedor1.ProveedorId equals pedido.Proveedor.ProveedorId
                        where pedido.PedidoId  == id
                        select new
                        {
                            Idpedido = pedido.PedidoId,
                            fechaentrega = pedido.Fechaentrega,
                            fechapedido = pedido.Fechapedido,
                            estado = pedido.Estado,
                            idproveedor = proveedor1.ProveedorId
                        };

            return Ok(query);
        }

        // POST: api/Pedido

        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <returns>JSON Pedido</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostPedido")]
        [Route("api/PostPedido")]
        public IHttpActionResult Post(Pedido pedido,  int idproveedor)
        {
            
            Proveedor proveedorexistente = db.Proveedores.Find(idproveedor);
            if (proveedorexistente == null)
            {
                return NotFound();
            }
            pedido.Proveedor = proveedorexistente;
            db.Pedidos.Add(pedido);
            db.SaveChanges();
            return Ok(pedido);
        }

        // PUT: api/Pedido/5

        /// <summary>
        /// modificar valores
        /// </summary>
        /// <returns>JSON Pedido</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutPedido")]
        [Route("api/PutPedido")]
        public IHttpActionResult Put(Pedido pedidomodificar, int idproveedor)
        {
            
            Proveedor proveedorexistente = db.Proveedores.Find(idproveedor);
            if (proveedorexistente == null)
            {
                return NotFound();
            }
            pedidomodificar.Proveedor = proveedorexistente;
            int id = pedidomodificar.PedidoId;
            db.Entry(pedidomodificar).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(pedidomodificar);
        }

        // DELETE: api/Pedido/5

        /// <summary>
        /// Eliminar valores
        /// </summary>
        /// <returns>JSON Pedido</returns>
        /// <response code="200">Devuelve si los valores son eliminados</response>
        /// <response code="404">Devuelve si no son eliminados</response>
        [HttpDelete]
        [SwaggerOperation("DeletePedido")]
        [Route("api/DeletePedido")]
        public IHttpActionResult Delete(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return Ok(pedido);

        }
    }
}
