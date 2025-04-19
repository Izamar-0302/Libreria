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
            var turno = db.Turnos
                .Include(l => l.Empleado)

                .Select(l => new
                {
                    l.TurnoId,
                    l.Horainicio,
                    l.Horafinal,
                    l.EmpleadoId,

                })
               .ToList();

            return Ok(turno);
        }


        // GET: api/Turno/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Turno</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetTurno")]
        [Route("api/GetTurno")]
        public IHttpActionResult Get(int id)
        {
            var turno  = db.Turnos
                .Include(l => l.Empleado)
                
                .Select(l => new
                {
                    l.TurnoId,
                    l.Horainicio,
                    l.Horafinal,
                    l.EmpleadoId,
                    
                })
               .FirstOrDefault();

            if (turno == null)
                return NotFound();

            return Ok(turno);
        }

        // POST: api/Turno

        /// <summary>
        /// Agregar
        /// </summary>
        /// <param name="turno"></param>
        /// <returns>JSON Turno</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostTurno")]
        [Route("api/PostTurno")]
        public IHttpActionResult Post(Turno turno)
        {


            if (turno == null)
                return BadRequest("Turno inválido.");

            var empleado = db.Empleados.Find(turno.EmpleadoId);
            

            if (empleado == null)
                return BadRequest("Empleado no encontrado.");
            
            db.Turnos.Add(turno);
            db.SaveChanges();
            return Ok(turno);
        }

        // PUT: api/Turno/5

        /// <summary>
        /// Modificar 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="turnomodificar"></param>
        /// <returns>JSON Turno</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutTurno")]
        [Route("api/PutTurno")]
        public IHttpActionResult Put(int id,Turno turnomodificar)
        {

            var turno = db.Turnos.Find(id);
            if (turno == null)
                return NotFound();


            var empleado = db.Empleados.Find(turnomodificar.EmpleadoId);


            if (empleado == null)
                return BadRequest("Empleado no encontrado.");



            turno.Horainicio = turnomodificar.Horainicio;
            turno.Horafinal = turnomodificar.Horafinal;
            

            turno.EmpleadoId = turnomodificar.EmpleadoId;


            db.SaveChanges();
            return Ok(turno);
        }

        // DELETE: api/Turno/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
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
