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
    public class VentaController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Venta
        /// <summary>
        /// Obtener todos los valores
        /// </summary>
        /// <returns>JSON Venta</returns>
        /// <response code="200">Devuelve si los valores son encontrados</response>
        /// <response code="404">Devuelve si no son encontrados</response>
        [HttpGet]
        [SwaggerOperation("GetVentas")]
        [Route("api/GetVentas")]
        public IHttpActionResult Get()
        {
            var Venta = db.Ventas
                .Include(l => l.Lector)
                .Include(l => l.Empleado)
                .Include(l => l.Pedido)
                .Include(l => l.Libro)
                .Include(l => l.Sucursal)
                .Select(l => new
                {
                    l.VentaId,
                    l.Fechadeventa,
                    l.LectorId,
                    l.EmpleadoId,
                    l.PedidoId,
                    l.LibroId,
                    l.MetodopagoId,
                    l.SucursalId,
                    l.Descripcion,
                    l.Cantidadventa,
                    l.Descuento,
                    l.Impuesto,
                    l.Montototal,
                    

                })
                .ToList();

            return Ok(Venta);
        }


        // GET: api/Venta/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Venta</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetVenta")]
        [Route("api/GetVenta")]
        public IHttpActionResult Get(int id)
        {
            var Venta = db.Ventas
                .Include(l => l.Lector)
                .Include(l => l.Empleado)
                .Include(l => l.Pedido)
                .Include(l => l.Libro)
                .Include(l => l.Sucursal)
                .Select(l => new
                {
                    l.VentaId,
                    l.Fechadeventa,
                    l.LectorId,
                    l.EmpleadoId,
                    l.PedidoId,
                    l.LibroId,
                    l.MetodopagoId,
                    l.SucursalId,
                    l.Descripcion,
                    l.Cantidadventa,
                    l.Descuento,
                    l.Impuesto,
                    l.Montototal,


                })
                .FirstOrDefault();

            if (Venta == null)
                return NotFound();

            return Ok(Venta);
        }

        // POST: api/Venta

        /// <summary>
        /// Agregar
        /// </summary>
        /// <param name="venta"></param>
        /// <returns>JSON Venta</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostVenta")]
        [Route("api/PostVenta")]
        public IHttpActionResult Post(Venta venta)
        {
            if (venta == null)
                return BadRequest("Detalle inválido.");

            var lector = db.Lectores.Find(venta.LectorId);
            var empleado = db.Empleados.Find(venta.EmpleadoId);
            var pedido = db.Pedidos.Find(venta.PedidoId);
            var libro = db.Libros.Find(venta.LibroId);
            var metodo = db.MetodosPagos.Find(venta.MetodopagoId);


            if (empleado == null)
                return BadRequest("Empleado no encontrado.");

            if (libro == null)
                return BadRequest("libro no encontrada.");
            if (lector == null)
                return BadRequest("lector no encontrada.");
            if (pedido == null)
            {
                venta.PedidoId = null;
               
             }
            if (metodo == null)
                return BadRequest("libro no encontrada.");


            venta.SucursalId = empleado.SucursalId;
            venta.Montototal = ((libro.Precio * venta.Cantidadventa) - (libro.Precio*venta.Descuento) + (libro.Precio*venta.Impuesto));
            db.Ventas.Add(venta);
            db.SaveChanges();
            return Ok(venta);
        }

        // PUT: api/Venta/5

        /// <summary>
        /// Modificar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ventamodificar"></param>
        /// <returns>JSON Venta</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutVenta")]
        [Route("api/PutVenta")]
        public IHttpActionResult Put(int id, Venta ventamodificar)
        {
            var venta = db.Ventas.Find(id);
            if (venta == null)
                return NotFound();

            var lector = db.Lectores.Find(ventamodificar.LectorId);
            var empleado = db.Empleados.Find(ventamodificar.EmpleadoId);
            var pedido = db.Pedidos.Find(ventamodificar.PedidoId);
            var libro = db.Libros.Find(ventamodificar.LibroId);
            var metodo = db.MetodosPagos.Find(ventamodificar.MetodopagoId);
           

            if (empleado == null)
                return BadRequest("Pedido no encontrado.");

            if (libro == null)
                return BadRequest("libro no encontrado.");
            if (lector == null)
                return BadRequest("lector no encontrado.");
            if (pedido == null)
                { venta.PedidoId = null; }
            if (metodo == null)
                return BadRequest("Metodo no encontrado.");
            

            venta.VentaId = venta.VentaId;
            venta.LibroId = ventamodificar.LibroId;
            venta.LectorId = ventamodificar.LectorId;
            venta.Fechadeventa = ventamodificar.Fechadeventa;
            venta.EmpleadoId = ventamodificar.EmpleadoId;
            venta.PedidoId = ventamodificar.PedidoId;
            venta.MetodopagoId = ventamodificar.MetodopagoId;
            venta.SucursalId = empleado.SucursalId;
            venta.Descripcion = ventamodificar.Descripcion;
            venta.Cantidadventa = ventamodificar.Cantidadventa;
            venta.Descuento = ventamodificar.Descuento;
            venta.Impuesto = ventamodificar.Impuesto;
            
            venta.Montototal = ((libro.Precio * venta.Cantidadventa) - (libro.Precio * venta.Descuento) + (libro.Precio * venta.Impuesto));
            
            db.SaveChanges();
            return Ok(venta);
        }


        // DELETE: api/Venta/5

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Venta</returns>
        /// <response code="200">Devuelve si los valores son eliminados</response>
        /// <response code="404">Devuelve si no son eliminados</response>
        [HttpDelete]
        [SwaggerOperation("DeleteVenta")]
        [Route("api/DeleteVenta")]
        public IHttpActionResult Delete(int id)
        {
            Venta venta = db.Ventas.Find(id);
            db.Ventas.Remove(venta);
            db.SaveChanges();
            return Ok(venta);

        }
    }
}
