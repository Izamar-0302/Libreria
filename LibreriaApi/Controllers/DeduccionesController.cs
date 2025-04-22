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
            var deduccion = db.Deducciones
                .Include(l => l.Empleado)

                .Select(l => new
                {
                    l.DeduccionesId,
                    l.EmpleadoId,
                    l.TipoDeduccion,
                    l.Monto,

                })
               .ToList();
               return Ok(deduccion);
        }


        // GET: api/Deducciones/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Deducciones</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetDeduccion")]
        [Route("api/GetDeduccion")]
        public IHttpActionResult Get(int id)
        {
            var deduccion = db.Deducciones
                .Include(l => l.Empleado)

                .Select(l => new
                {
                    l.DeduccionesId,
                    l.EmpleadoId,
                    l.TipoDeduccion,
                    l.Monto,

                })
               .FirstOrDefault();

            if (deduccion == null)
                return NotFound();

            return Ok(deduccion);
        }

        // POST: api/Deducciones

        /// <summary>
        /// Agregar
        /// </summary>
        /// <param name="deducciones"></param>
        /// <returns>JSON Deducciones</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostDeducciones")]
        [Route("api/PostDeducciones")]
        public IHttpActionResult Post(Deducciones deducciones)
        {
            if (deducciones == null)
                return BadRequest("Deduccion inválida.");

            var empleado = db.Empleados.Find(deducciones.EmpleadoId);


            if (empleado == null)
                return BadRequest("Empleado no encontrado.");

            db.Deducciones.Add(deducciones);
            db.SaveChanges();
            return Ok(deducciones);
        }

        // PUT: api/Deducciones/5

        /// <summary>
        /// Modificar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dedumodificar"></param>
        /// <returns>JSON Deducciones</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutDeducciones")]
        [Route("api/PutDeducciones")]
        public IHttpActionResult Put(int id,Deducciones dedumodificar)
        {

            var deduccion = db.Deducciones.Find(id);
            if (deduccion == null)
                return NotFound();


            var empleado = db.Empleados.Find(dedumodificar.EmpleadoId);


            if (empleado == null)
                return BadRequest("Empleado no encontrado.");



            deduccion.TipoDeduccion = dedumodificar.TipoDeduccion;
            deduccion.Monto = dedumodificar.Monto;


            deduccion.EmpleadoId = dedumodificar.EmpleadoId;


            db.SaveChanges();
            return Ok(deduccion);
        }

        // DELETE: api/Deducciones/5

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
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
        [Route("api/Deducciones/Ordenadopormonton")]
        public IHttpActionResult Ordenadopormonton()
        {
            var que = from empleado1 in db.Empleados
                      join deducciones in db.Deducciones on empleado1.EmpleadoId equals deducciones.EmpleadoId
                      orderby deducciones.Monto descending
                      select new
                      {
                          Iddeducciones = deducciones.DeduccionesId,
                          tipodeducciones = deducciones.TipoDeduccion,
                          Idempleado = empleado1.EmpleadoId,
                          monton = deducciones.Monto,

                      };
            return Ok(que);
        }
    }
}
