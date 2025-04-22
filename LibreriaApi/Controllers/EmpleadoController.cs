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


        public IHttpActionResult Get()
        {
            var empleados = db.Empleados
                .Include(l => l.Cargo)
                .Include(l => l.Sucursal)
                .Select(l => new
                {
                    l.EmpleadoId,
                    l.Nombre,
                    l.Apellidos,
                    l.Correo,
                    l.Telefono,
                    l.Direccion,
                    l.CargoId,
                    l.SucursalId,
                    l.Salario
                })
                .ToList();

            return Ok(empleados);


        
        
        }
        // GET: api/Empleado/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Empleado</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetEmpleado")]
        [Route("api/GetEmpleado")]
        public IHttpActionResult Get(int id)
        {
            var empleados = db.Empleados
                .Include(l => l.Cargo)
                .Include(l => l.Sucursal)
                .Select(l => new
                {
                    l.EmpleadoId,
                    l.Nombre,
                    l.Apellidos,
                    l.Correo,
                    l.Telefono,
                    l.Direccion,
                    l.CargoId,
                    l.SucursalId,
                    l.Salario
                })
               .FirstOrDefault();

            if (empleados == null)
                return NotFound();

            return Ok(empleados);
        }

        // POST: api/Empleado

        /// <summary>
        /// Agregar
        /// </summary>
        /// <param name="empleado"></param>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostEmpleado")]
        [Route("api/PostEmpleado")]
        public IHttpActionResult Post(Empleado empleado)
        {
            if (empleado == null)
                return BadRequest("Empleado inválido.");

            var sucursal = db.Sucursales.Find(empleado.SucursalId);
            var cargo = db.Cargo.Find(empleado.CargoId);

            if (sucursal == null)
                return BadRequest("Sucursal no encontrada.");

            if (cargo == null)
                return BadRequest("Cargo no encontrado.");

            db.Empleados.Add(empleado);
            db.SaveChanges();

            return Ok(empleado);
        }

        // PUT: api/Empleado/5

        /// <summary>
        /// Modificar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empleadomodificar"></param>
        /// <returns>JSON Empleado</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutEmpleado")]
        [Route("api/PutEmpleado")]
        public IHttpActionResult Put(int id, Empleado empleadomodificar)
        {
            var empleado = db.Empleados.Find(id);
            if (empleado == null)
                return NotFound();


            var sucursal = db.Sucursales.Find(empleadomodificar.SucursalId);
            var cargo = db.Cargo.Find(empleadomodificar.CargoId);

            if (sucursal == null)
                return BadRequest("Sucursal no encontrada.");
            if (cargo == null)
                return BadRequest("Cargo no encontrado.");


            empleado.Nombre = empleadomodificar.Nombre;
            empleado.Apellidos = empleadomodificar.Apellidos;
            empleado.Correo = empleadomodificar.Correo;
            empleado.Telefono = empleadomodificar.Telefono;
            empleado.Direccion = empleadomodificar.Direccion;


            empleado.SucursalId = empleadomodificar.SucursalId;
            empleado.CargoId = empleadomodificar.CargoId;


            empleado.Salario = empleadomodificar.Salario;

            db.SaveChanges();
            return Ok(empleado);
        }

        // DELETE: api/Empleado/5

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
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

        [Route("api/Empleado/Ordenadoporsucursalysalario")]
        public IHttpActionResult Ordenadoporsucursalysalario()
        {
            var que = from cargo1 in db.Cargo
                      join empleado in db.Empleados on cargo1.CargoId equals empleado.CargoId
                      join sucursal1 in db.Sucursales on empleado.SucursalId equals sucursal1.SucursalId
                      orderby sucursal1.Nombresucursal, empleado.Salario
                      select new
                      {
                          Iddempleado =  empleado.EmpleadoId,
                          Idcargo = cargo1.CargoId,
                          Idsucursal = sucursal1.SucursalId,
                          salario = empleado.Salario
                          

                      };
            return Ok(que);
        }
    }
}
