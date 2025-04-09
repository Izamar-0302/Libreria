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
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Venta</returns>
        /// <response code="200">Devuelve si los valores son encontrados</response>
        /// <response code="404">Devuelve si no son encontrados</response>
        [HttpGet]
        [SwaggerOperation("GetVentas")]
        [Route("api/GetVentas")]
        public IHttpActionResult Get()
        {
            var query = from lector1 in db.Lectores
                        join venta in db.Ventas on lector1.LectorId equals venta.Lector.LectorId
                        join empleado1 in db.Empleados on venta.Empleado.EmpleadoId equals empleado1.EmpleadoId
                        join pedido1 in db.Pedidos on venta.Pedido.PedidoId equals pedido1.PedidoId
                        join libro1 in db.Libros on venta.Libro.LibroId equals libro1.LibroId
                        join metodopago1 in db.MetodosPagos on venta.MetodoPago.MetodopagoId equals metodopago1.MetodopagoId
                        join sucursal1 in db.Sucursales on venta.Sucursal.SucursalId equals sucursal1.SucursalId

                        select new
                        {
                            Idventa = venta.VentaId,
                            fechadeventa = venta.Fechadeventa,
                            IdLector = lector1.LectorId,
                            IdEmpleado = empleado1.EmpleadoId,
                            IdPedido = pedido1.PedidoId,
                            IdLibro = libro1.LibroId,
                            IdMetododepago = metodopago1.MetodopagoId,
                            IdSucursal = sucursal1.SucursalId,
                            descripcion = venta.Descripcion,
                            Cantidad = venta.Cantidadventa,
                            montontotal = venta.Montototal,
                            descuento = venta.Descuento,
                            impuesto = venta.Impuesto
                        };

            return Ok(query);
        }


        // GET: api/Venta/5

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <returns>JSON Venta</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetVenta")]
        [Route("api/GetVenta")]
        public IHttpActionResult Get(int id)
        {
            var query = from lector1 in db.Lectores
                        join venta in db.Ventas on lector1.LectorId equals venta.Lector.LectorId
                        join empleado1 in db.Empleados on venta.Empleado.EmpleadoId equals empleado1.EmpleadoId
                        join pedido1 in db.Pedidos on venta.Pedido.PedidoId equals pedido1.PedidoId
                        join libro1 in db.Libros on venta.Libro.LibroId equals libro1.LibroId
                        join metodopago1 in db.MetodosPagos on venta.MetodoPago.MetodopagoId equals metodopago1.MetodopagoId
                        join sucursal1 in db.Sucursales on venta.Sucursal.SucursalId equals sucursal1.SucursalId
                        where venta.VentaId == id
                        select new
                        {
                            Idventa = venta.VentaId,
                            fechadeventa = venta.Fechadeventa,
                            IdLector = lector1.LectorId,
                            IdEmpleado = empleado1.EmpleadoId,
                            IdPedido = pedido1.PedidoId,
                            IdLibro = libro1.LibroId,
                            IdMetododepago = metodopago1.MetodopagoId,
                            IdSucursal = sucursal1.SucursalId,
                            descripcion = venta.Descripcion,
                            Cantidad = venta.Cantidadventa,
                            montontotal = venta.Montototal,
                            descuento = venta.Descuento,
                            impuesto = venta.Impuesto
                        };

            return Ok(query);
        }

        // POST: api/Venta

        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <returns>JSON Venta</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostVenta")]
        [Route("api/PostVenta")]
        public IHttpActionResult Post(Venta venta, int idlector, int idempleado, int idpedido, int idlibro, int idmetododepago)
        {
            Lector lectorexistente = db.Lectores.Find(idlector);
            if (lectorexistente == null)
            {
                return NotFound();
            }
            venta.Lector = lectorexistente;
            Empleado empleadoexistente = db.Empleados.Find(idempleado);
            if (empleadoexistente == null)
            {
                return NotFound();
            }
            venta.Empleado = empleadoexistente;
            venta.Sucursal = empleadoexistente.Sucursal;
            Pedido pedidoexistente = db.Pedidos.Find(idpedido);
            if (pedidoexistente == null)
            {
                return NotFound();
            }
            venta.Pedido = pedidoexistente;
            Libro libroexistente = db.Libros.Find(idlibro);
            if (libroexistente == null)
            {
                return NotFound();
            }
            venta.Libro = libroexistente;
            
            MetodoPago mdpexistente = db.MetodosPagos.Find(idmetododepago);
            if (mdpexistente == null)
            {
                return NotFound();
            }
            venta.MetodoPago = mdpexistente;
            
            venta.Montototal = ((libroexistente.Precio * venta.Cantidadventa) - venta.Descuento + venta.Impuesto);
            db.Ventas.Add(venta);
            db.SaveChanges();
            return Ok(venta);
        }

        // PUT: api/Venta/5

        /// <summary>
        /// modificar valores
        /// </summary>
        /// <returns>JSON Venta</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutVenta")]
        [Route("api/PutVenta")]
        public IHttpActionResult Put(Venta ventamodificar, int idlector, int idempleado, int idpedido, int idlibro, int idmetododepago)
        {
            Lector lectorexistente = db.Lectores.Find(idlector);
            if (lectorexistente == null)
            {
                return NotFound();
            }
            ventamodificar.Lector = lectorexistente;
            Empleado empleadoexistente = db.Empleados.Find(idempleado);
            if (empleadoexistente == null)
            {
                return NotFound();
            }
            ventamodificar.Empleado = empleadoexistente;
            ventamodificar.Sucursal = empleadoexistente.Sucursal;
            Pedido pedidoexistente = db.Pedidos.Find(idpedido);
            if (pedidoexistente == null)
            {
                return NotFound();
            }
            ventamodificar.Pedido = pedidoexistente;
            Libro libroexistente = db.Libros.Find(idlibro);
            if (libroexistente == null)
            {
                return NotFound();
            }
            ventamodificar.Libro = libroexistente;
            MetodoPago mdpexistente = db.MetodosPagos.Find(idmetododepago);
            if (mdpexistente == null)
            {
                return NotFound();
            }
            ventamodificar.MetodoPago = mdpexistente;
            
            ventamodificar.Montototal = ((libroexistente.Precio * ventamodificar.Cantidadventa) - ventamodificar.Descuento + ventamodificar.Impuesto);
            int id = ventamodificar.VentaId;
            db.Entry(ventamodificar).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(ventamodificar);
        }

        // DELETE: api/Venta/5

        /// <summary>
        /// Eliminar valores
        /// </summary>
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
