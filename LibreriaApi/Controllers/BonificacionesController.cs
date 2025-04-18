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
    public class BonificacionesController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Bonificaciones
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Bonificaciones</returns>
        /// <response code="200">Devuelve si los valores son encontrados</response>
        /// <response code="404">Devuelve si no son encontrados</response>
        [HttpGet]
        [SwaggerOperation("GetBonificaciones")]
        [Route("api/GetBonificaciones")]
        public IHttpActionResult Get()
        {
            var bonificaciones = db.Bonificaciones
                .Include(l => l.Empleado)
                .Select(l => new
                {
                    l.BonificacionesId,
                    l.EmpleadoId,
                    l.Tipobonificacion,
                    l.Monto,
                })
                .ToList();

            return Ok(bonificaciones);

        }


        // GET: api/Bonificaciones/5

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <returns>JSON Bonificaciones</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        ///  [HttpGet]
        [SwaggerOperation("GetBonificacion")]
        [Route("api/GetBonificacion")]
        public IHttpActionResult Get(int id)
        {
            var bonificaciones = db.Bonificaciones
                           .Include(l => l.Empleado)
                           .Select(l => new
                           {
                               l.BonificacionesId,
                               l.EmpleadoId,
                               l.Tipobonificacion,
                               l.Monto,
                           })
                           .FirstOrDefault();

            if (bonificaciones == null)
                return NotFound();

            return Ok(bonificaciones);
        }

        // POST: api/Bonificaciones
        
        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <returns>JSON Bonificaciones</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostBonificaciones")]
        [Route("api/PostBonificaciones")]
        public IHttpActionResult Post(Bonificaciones bonificacion)
        {
            if (bonificacion == null)
                return BadRequest("Bonificaciones son inválidas.");

            var empleado = db.Empleados.Find(bonificacion.EmpleadoId);
            
            if (empleado == null)
                return BadRequest("Empleado no encontrada.");

          

            db.Bonificaciones.Add(bonificacion);
            db.SaveChanges();

            return Ok(bonificacion);
        }

        // PUT: api/Bonificaciones/5
       
        /// <summary>
        /// modificar valores
        /// </summary>
        /// <returns>JSON Bonificaciones</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutBonificacion")]
        [Route("api/PutBonificacion")]
        public IHttpActionResult Put(int id, Bonificaciones bonificacionmodificar)
        {
            var bonificacion = db.Bonificaciones.Find(id);
            if (bonificacion == null)
                return NotFound();

          
            var empleado = db.Empleados.Find(bonificacionmodificar.EmpleadoId);
            if (empleado == null)
                return BadRequest("Empleado no encontrado.");

          
            bonificacion.EmpleadoId = bonificacionmodificar.EmpleadoId;
            bonificacion.Tipobonificacion = bonificacionmodificar.Tipobonificacion;
            bonificacion.Monto = bonificacionmodificar.Monto;

            db.SaveChanges();
            return Ok(bonificacion);
        }

        // DELETE: api/Bonificaciones/5

        /// <summary>
        /// Eliminar valores
        /// </summary>
        /// <returns>JSON Bonificaciones</returns>
        /// <response code="200">Devuelve si los valores son eliminados</response>
        /// <response code="404">Devuelve si no son eliminados</response>
        [HttpDelete]
        [SwaggerOperation("DeleteBonificacion")]
        [Route("api/DeleteBonificacion")]
        public IHttpActionResult Delete(int id)
        {
            Bonificaciones bonificacion = db.Bonificaciones.Find(id);
            db.Bonificaciones.Remove(bonificacion);
            db.SaveChanges();
            return Ok(bonificacion);

        }
    }
}
