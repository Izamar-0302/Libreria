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
            var pedido = db.Pedidos
                .Include(l => l.Proveedor)
              
                .Select(l => new
                {
                    l.PedidoId,
                    l.ProveedorId,
                    l.Fechapedido,
                    l.Fechaentrega,
                    l.Estado,
                    
                })
                .ToList();

            return Ok(pedido);

        }

        // GET: api/Pedido/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Pedido</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetPedido")]
        [Route("api/GetPedido")]
        public IHttpActionResult Get(int id)
        {
            var pedido = db.Pedidos
                .Include(l => l.Proveedor)

                .Select(l => new
                {
                    l.PedidoId,
                    l.ProveedorId,
                    l.Fechapedido,
                    l.Fechaentrega,
                    l.Estado,
                    Proveedor = new Proveedor
                    {
                        Nombreproveedor = l.Proveedor.Nombreproveedor
                    }
                })
                .FirstOrDefault();

            if (pedido == null)
                return NotFound();
            return Ok(pedido);
        }

        // POST: api/Pedido

        /// <summary>
        /// Agregar
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns>JSON Pedido</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostPedido")]
        [Route("api/PostPedido")]
        public IHttpActionResult Post(Pedido pedido)
        {
            
           
            if (pedido.Proveedor != null)
            {
                Proveedor Proveedor = db.Proveedores.Find(pedido.Proveedor.ProveedorId);
                pedido.Proveedor = Proveedor;
            }
           
            db.Pedidos.Add(pedido);
            db.SaveChanges();
            return Ok(pedido);
        }

        // PUT: api/Pedido/5

        /// <summary>
        /// Modificar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pedidomodificar"></param>
        /// <returns>JSON Pedido</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutPedido")]
        [Route("api/PutPedido")]
        public IHttpActionResult Put(int id,Pedido pedidomodificar)
        {
            var Pedido = db.Pedidos.Find(id);
            if (Pedido == null)
                return NotFound();


            var proveedor = db.Proveedores.Find(pedidomodificar.ProveedorId);
           

            if (proveedor == null)
                return BadRequest("Proveedor no encontrado.");



            Pedido.Estado = pedidomodificar.Estado;
            Pedido.Fechaentrega = pedidomodificar.Fechaentrega;
            Pedido.Fechapedido = pedidomodificar.Fechapedido;


            Pedido.ProveedorId= pedidomodificar.ProveedorId;
            

            db.SaveChanges();
            return Ok(Pedido);
        }

        // DELETE: api/Pedido/5

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
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

        [Route("api/Empleado/Ordenadoporentrega")]
        public IHttpActionResult Ordenadoporentrega()
        {
            var que = from proveedor1 in db.Proveedores
                      join pedido in db.Pedidos on proveedor1.ProveedorId equals pedido.ProveedorId
                      orderby pedido.Fechaentrega descending
                      select new
                      {
                          pedido.PedidoId,
                          proveedor1.ProveedorId,
                          pedido.Fechapedido,
                          pedido.Fechaentrega,
                          pedido.Estado


                      };
            return Ok(que);
        }

        [Route("api/Pedido/Ordenadoporestado")]
        public IHttpActionResult Ordenadoporestado()
        {
            var que = from proveedor1 in db.Proveedores
                      join pedido in db.Pedidos on proveedor1.ProveedorId equals pedido.ProveedorId
                      orderby pedido.Estado ascending
                      select new
                      {
                          pedido.PedidoId,
                          proveedor1.ProveedorId,
                          pedido.Fechapedido,
                          pedido.Fechaentrega,
                          pedido.Estado


                      };
            return Ok(que);
        }
    }
}
