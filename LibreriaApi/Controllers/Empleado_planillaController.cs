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
    public class Empleado_planillaController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Empleado_planilla
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Empleado_planilla</returns>
        /// <response code="200">Devuelve si los valores son encontrados</response>
        /// <response code="404">Devuelve si no son encontrados</response>
        [HttpGet]
        [SwaggerOperation("GetEmpleado_planillas")]
        [Route("api/GetEmpleado_planillas")]
        public IHttpActionResult Get()
        {
            var query = from empleado1 in db.Empleados
                        join empleado_planilla in db.Empleados_Planillas on empleado1.Id equals empleado_planilla.Empleado.Id
                        join bonificaciones1 in db.Bonificaciones on empleado_planilla.Bono.Id equals bonificaciones1.Id
                        join deducciones1 in db.Deducciones on empleado_planilla.Deduccion.Id equals deducciones1.Id
                        
                        select new
                        {
                            Idempleadoplanilla = empleado_planilla.Id,
                            Nombremepleado = empleado1.Nombre,
                            anticipo = empleado_planilla.Anticipo,
                            bonificacion = bonificaciones1.Monto,
                            deduccionesmonto = deducciones1.Monto,
                            sueldobase = empleado1.Salario,
                            sueldoneto = empleado_planilla.Sueldoneto

                        };

            return Ok(query);
        }


        // GET: api/Empleado_planilla/5

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <returns>JSON Empleado_planilla</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetEmpleado_planilla")]
        [Route("api/GetEmpleado_planilla")]
        public IHttpActionResult Get(int id)
        {
            var query = from empleado1 in db.Empleados
                        join empleado_planilla in db.Empleados_Planillas on empleado1.Id equals empleado_planilla.Empleado.Id
                        join bonificaciones1 in db.Bonificaciones on empleado_planilla.Bono.Id equals bonificaciones1.Id
                        join deducciones1 in db.Deducciones on empleado_planilla.Deduccion.Id equals deducciones1.Id
                        where empleado_planilla.Id == id
                        select new
                        {
                            Idempleadoplanilla = empleado_planilla.Id,
                            Nombremepleado = empleado1.Nombre,
                            anticipo = empleado_planilla.Anticipo,
                            bonificacion = bonificaciones1.Monto,
                            deduccionesmonto = deducciones1.Monto,
                            sueldobase = empleado1.Salario,
                            sueldoneto = empleado_planilla.Sueldoneto

                        };

            return Ok(query);
        }

        // POST: api/Empleado_planilla

        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <returns>JSON Empleado_planilla</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostEmpleado_planilla")]
        [Route("api/PostEmpleado_planilla")]
        public IHttpActionResult Post(Empleado_planilla empleado_planilla, int idempleado, int idbonificacion, int iddeduccion)
        {
            Empleado empleadoexistente = db.Empleados.Find(idempleado);
            
            if (empleadoexistente == null)
            {
                return NotFound();
            }
            empleado_planilla.Empleado = empleadoexistente;
            Bonificaciones bonificacionexistente = db.Bonificaciones.Find(idbonificacion);
            if ( bonificacionexistente == null)
            {
                return NotFound();
            }
            empleado_planilla.Bono = bonificacionexistente;
            Deducciones deduccionexistente = db.Deducciones.Find(iddeduccion);
            if (deduccionexistente == null)
            {
                return NotFound();
            }
            empleado_planilla.Deduccion = deduccionexistente;
            
            empleado_planilla.Sueldoneto = (empleadoexistente.Salario - empleado_planilla.Anticipo + bonificacionexistente.Monto  - deduccionexistente.Monto);
            db.Empleados_Planillas.Add(empleado_planilla);
            db.SaveChanges();
            return Ok(empleado_planilla);
        }

        // PUT: api/Empleado_planilla/5

        /// <summary>
        /// modificar valores
        /// </summary>
        /// <returns>JSON Empleado_planilla</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutEmpleado_planilla")]
        [Route("api/PutEmpleado_planilla")]
        public IHttpActionResult Put(Empleado_planilla epmodificar, int idempleado, int idbonificacion, int iddeduccion)
        {
            Empleado empleadoexistente = db.Empleados.Find(idempleado);
            if (empleadoexistente == null)
            {
                return NotFound();
            }
            epmodificar.Empleado = empleadoexistente;
            Bonificaciones bonificacionexistente = db.Bonificaciones.Find(idbonificacion);
            if (bonificacionexistente == null)
            {
                return NotFound();
            }
            epmodificar.Bono = bonificacionexistente;
            Deducciones deduccionexistente = db.Deducciones.Find(iddeduccion);
            if (deduccionexistente == null)
            {
                return NotFound();
            }
            epmodificar.Deduccion = deduccionexistente;
            epmodificar.Sueldoneto = (empleadoexistente.Salario - epmodificar.Anticipo + bonificacionexistente.Monto - deduccionexistente.Monto);
            int id = epmodificar.Id;
            db.Entry(epmodificar).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(epmodificar);
        }

        // DELETE: api/Empleado_planilla/5

        /// <summary>
        /// Eliminar valores
        /// </summary>
        /// <returns>JSON Empleado_planilla</returns>
        /// <response code="200">Devuelve si los valores son eliminados</response>
        /// <response code="404">Devuelve si no son eliminados</response>
        [HttpDelete]
        [SwaggerOperation("DeleteEmpleado_planilla")]
        [Route("api/DeleteEmpleado_planilla")]
        public IHttpActionResult Delete(int id)
        {
            Empleado_planilla empleado_planilla = db.Empleados_Planillas.Find(id);
            db.Empleados_Planillas.Remove(empleado_planilla);
            db.SaveChanges();
            return Ok(empleado_planilla);

        }
    }
}
