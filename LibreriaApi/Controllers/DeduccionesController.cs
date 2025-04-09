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
    public class DeduccionesController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Deducciones
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Deducciones</returns>
        /// <response code="200">Devuelve si los valores son encontrados</response>
        /// <response code="404">Devuelve si no son encontrados</response>
        [HttpGet]
        [SwaggerOperation("GetDeducciones")]
        [Route("api/GetDeducciones")]
        public IHttpActionResult Get()
        {
            var query = from empleado1 in db.Empleados
                        join deducciones in db.Deducciones
                                on empleado1.EmpleadoId equals deducciones.Empleado.EmpleadoId
                        select new
                        {
                            Iddeducciones = deducciones.DeduccionesId,
                            Tipodeducciones = deducciones.TipoDeduccion,
                            Montondeducciones = deducciones.Monto,
                            IdEmpleado = empleado1.EmpleadoId
                        };

            return Ok(query);
        }


        // GET: api/Deducciones/5

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <returns>JSON Deducciones</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
         [HttpGet]
        [SwaggerOperation("GetDeduccion")]
        [Route("api/GetDeduccion")]
        public IHttpActionResult Get(int id)
        {
            var query = from empleado1 in db.Empleados
                        join deducciones in db.Deducciones
                              on empleado1.EmpleadoId equals deducciones.Empleado.EmpleadoId
                        where deducciones.DeduccionesId == id
                        select new
                        {
                            Iddeducciones = deducciones.DeduccionesId,
                            Tipodeducciones = deducciones.TipoDeduccion,
                            Montondeducciones = deducciones.Monto,
                            IdEmpleado = empleado1.EmpleadoId
                        };
            return Ok(query);
        }

        // POST: api/Deducciones

        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <returns>JSON Deducciones</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostDeducciones")]
        [Route("api/PostDeducciones")]
        public IHttpActionResult Post(Deducciones deducciones, int idempleado)
        {
            Empleado empleadoexistente = db.Empleados.Find(idempleado);
            if (empleadoexistente == null)
            {
                return NotFound();
            }
            deducciones.Empleado = empleadoexistente;
            db.Deducciones.Add(deducciones);
            db.SaveChanges();
            return Ok(deducciones);
        }

        // PUT: api/Deducciones/5

        /// <summary>
        /// modificar valores
        /// </summary>
        /// <returns>JSON Deducciones</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutDeducciones")]
        [Route("api/PutDeducciones")]
        public IHttpActionResult Put(Deducciones dedumodificar, int idempleado)
        {

            Empleado empleadoexistente = db.Empleados.Find(idempleado);
            if (empleadoexistente == null)
            {
                return NotFound();
            }
            dedumodificar.Empleado = empleadoexistente;
            int id = dedumodificar.DeduccionesId;
            db.Entry(dedumodificar).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(dedumodificar);
        }

        // DELETE: api/Deducciones/5

        /// <summary>
        /// Eliminar valores
        /// </summary>
        /// <returns>JSON Deducciones</returns>
        /// <response code="200">Devuelve si los valores son eliminados</response>
        /// <response code="404">Devuelve si no son eliminados</response>
        [HttpDelete]
        [SwaggerOperation("DeleteDeducciones")]
        [Route("api/DeleteDeducciones")]
        public IHttpActionResult Delete(int id)
        {
            Deducciones deducciones = db.Deducciones.Find(id);
            db.Deducciones.Remove(deducciones);
            db.SaveChanges();
            return Ok(deducciones);

        }
    }
}
