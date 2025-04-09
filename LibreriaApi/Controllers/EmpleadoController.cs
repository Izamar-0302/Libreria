using LibreriaApi.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibreriaApi.Controllers
{
    public class EmpleadoController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Empleado
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Empleado</returns>
        /// <response code="200">Devuelve si los valores son encontrados</response>
        /// <response code="404">Devuelve si no son encontrados</response>
        [HttpGet]
        [SwaggerOperation("GetEmpleados")]
        [Route("api/GetEmpleados")]
        
        public IEnumerable<Empleado> Get()
        {
            return db.Empleados;
        }


        // GET: api/Empleado/5

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <returns>JSON Empleado</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetEmpleado")]
        [Route("api/GetEmpleado")]
        public IHttpActionResult Get(int id)
        {
            var query = from cargo1 in db.Cargo
                        join empleado in db.Empleados on cargo1.CargoId equals empleado.Cargo.CargoId
                        join sucursal1 in db.Sucursales on empleado.Sucursal.SucursalId equals sucursal1.SucursalId
                        where empleado.EmpleadoId == id
                        select new
                        {
                            IdEmpleados = empleado.EmpleadoId,
                            TipoBonificacion = empleado.Nombre,
                            MontonBonificacion = empleado.Apellidos,
                            IdEmpleado = empleado.Direccion,
                            empleado.Correo,
                            empleado.Cargo,
                            empleado.Sucursal,
                            empleado.Telefono,
                            empleado.Salario

                        };

            return Ok(query);
        }

        // POST: api/Empleado

        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <returns>JSON Empleado</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostEmpleado")]
        [Route("api/PostEmpleado")]
        public IHttpActionResult Post(Empleado empleado)
        {
            if (empleado.Sucursal != null)
            {
                Sucursal sucursal = db.Sucursales.Find(empleado.Sucursal.SucursalId);
                empleado.Sucursal = sucursal;
            }
            if (empleado.Cargo != null)
            {
                Cargo cargo = db.Cargo.Find(empleado.Cargo.CargoId);
                empleado.Cargo = cargo;
            }
            db.Empleados.Add(empleado);
            db.SaveChanges();

            return Ok(empleado);
        }

        // PUT: api/Empleado/5

        /// <summary>
        /// modificar valores
        /// </summary>
        /// <returns>JSON Empleado</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutEmpleado")]
        [Route("api/PutEmpleado")]
        public IHttpActionResult Put(Empleado empleadomodificar, int idcargo, int idsucursal)
        {
            Cargo cargoexistente = db.Cargo.Find(idcargo);
            if (cargoexistente == null)
            {
                return NotFound();
            }
            empleadomodificar.Cargo = cargoexistente;
            Sucursal sucursalexistente = db.Sucursales.Find(idsucursal);
            if (sucursalexistente == null)
            {
                return NotFound();
            }
            empleadomodificar.Sucursal = sucursalexistente;
            int id = empleadomodificar.EmpleadoId;
            db.Entry(empleadomodificar).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(empleadomodificar);
        }

        // DELETE: api/Empleado/5

        /// <summary>
        /// Eliminar valores
        /// </summary>
        /// <returns>JSON Empleado</returns>
        /// <response code="200">Devuelve si los valores son eliminados</response>
        /// <response code="404">Devuelve si no son eliminados</response>
        [HttpDelete]
        [SwaggerOperation("DeleteEmpleado")]
        [Route("api/DeleteEmpleado")]
        public IHttpActionResult Delete(int id)
        {
            Empleado empleado = db.Empleados.Find(id);
            db.Empleados.Remove(empleado);
            db.SaveChanges();
            return Ok(empleado);

        }
    }
}
