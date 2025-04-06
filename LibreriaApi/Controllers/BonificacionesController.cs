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
            var query = from empleado1 in db.Empleados
                        join bonificaciones in db.Bonificaciones
                                on empleado1.Id equals bonificaciones.Empleado.Id
                        select new
                        {
                            IdBonificacion = bonificaciones.Id,
                            TipoBonificacion = bonificaciones.Tipobonificacion,
                            MontonBonificacion = bonificaciones.Monto,
                            IdEmpleado = empleado1.Id
                        };

           return Ok(query);
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
            var query = from empleado1 in db.Empleados
                        join bonificacion in db.Bonificaciones
                              on empleado1.Id equals bonificacion.Empleado.Id
                        where bonificacion.Id==id
                        select new
                        {
                            IdBonificacion = bonificacion.Id,
                            TipoBonificacion = bonificacion.Tipobonificacion,
                            MontonBonificacion = bonificacion.Monto,
                            IdEmpleado = empleado1.Id
                        };
            return Ok(query);
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
        public IHttpActionResult Post(Bonificaciones bonificacion,int idempleado )
        {
            if (bonificacion.Empleado != null)
            {
                Empleado empleadoexistente = db.Empleados.Find(bonificacion.Empleado.Id);
                bonificacion.Empleado = empleadoexistente;
            }
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
        public IHttpActionResult Put(Bonificaciones bonimodificar)
        {
            
            
            if (bonimodificar.Empleado != null)
            {
                Empleado empleadoexistente = db.Empleados.Find(bonimodificar.Empleado.Id);
                bonimodificar.Empleado = empleadoexistente;
            }
            
            int id = bonimodificar.Id;
            db.Entry(bonimodificar).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(bonimodificar);
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
