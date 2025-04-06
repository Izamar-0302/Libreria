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
    public class TurnoController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Turno
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Turno</returns>
        /// <response code="200">Devuelve si los valores son encontrados</response>
        /// <response code="404">Devuelve si no son encontrados</response>
        [HttpGet]
        [SwaggerOperation("GetTurnos")]
        [Route("api/GetTurnos")]
        public IHttpActionResult Get()
        {
            var query = from empleado1 in db.Empleados
                        join turno in db.Turnos on empleado1.Id equals turno.Empleado.Id
                        select new
                        {
                            Idturnoo = turno.TurnoId,
                            idEmpleado = empleado1.Id,
                            horaInicio = turno.Horainicio,
                            horaFinal = turno.Horafinal,
                            
                        };

            return Ok(query);
        }


        // GET: api/Turno/5

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <returns>JSON Turno</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetTurno")]
        [Route("api/GetTurno")]
        public IHttpActionResult Get(int id)
        {
            var query = from empleado1 in db.Empleados
                        join turno in db.Turnos on empleado1.Id equals turno.Empleado.Id
                        where turno.TurnoId == id
                        select new
                        {
                            Idturnoo = turno.TurnoId,
                            idEmpleado = empleado1.Id,
                            horaInicio = turno.Horainicio,
                            horaFinal = turno.Horafinal,

                        };

            return Ok(query);
        }

        // POST: api/Turno

        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <returns>JSON Turno</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostTurno")]
        [Route("api/PostTurno")]
        public IHttpActionResult Post(Turno turno, int idempleado)
        {

            Empleado empleadoexistente = db.Empleados.Find(idempleado);
            if (empleadoexistente == null)
            {
                return NotFound();
            }
            turno.Empleado = empleadoexistente;
            db.Turnos.Add(turno);
            db.SaveChanges();
            return Ok(turno);
        }

        // PUT: api/Turno/5

        /// <summary>
        /// modificar valores
        /// </summary>
        /// <returns>JSON Turno</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutTurno")]
        [Route("api/PutTurno")]
        public IHttpActionResult Put(Turno turnomodificar, int idempleado)
        {

            Empleado empleadoexistente = db.Empleados.Find(idempleado);
            if (empleadoexistente == null)
            {
                return NotFound();
            }
            turnomodificar.Empleado = empleadoexistente;
            
            int id = turnomodificar.TurnoId;
            db.Entry(turnomodificar).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(turnomodificar);
        }

        // DELETE: api/Turno/5

        /// <summary>
        /// Eliminar valores
        /// </summary>
        /// <returns>JSON Turno</returns>
        /// <response code="200">Devuelve si los valores son eliminados</response>
        /// <response code="404">Devuelve si no son eliminados</response>
        [HttpDelete]
        [SwaggerOperation("DeleteTurno")]
        [Route("api/DeleteTurno")]
        public IHttpActionResult Delete(int id)
        {
            Turno turno = db.Turnos.Find(id);
            db.Turnos.Remove(turno);
            db.SaveChanges();
            return Ok(turno);

        }
    }
}
