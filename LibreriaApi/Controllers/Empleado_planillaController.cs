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
            var empleadoplanilla = db.Empleados_Planillas
                .Include(l => l.Empleado)
                .Include(l => l.Bono)
                .Include(l => l.Deduccion)
                .Select(l => new
                {
                    l.Id,
                    l.EmpleadoId,
                    l.BonificacionesId,
                    l.Anticipo,
                    l.DeduccionesId,
                    l.Sueldoneto,
                   
                })
                .ToList();

            return Ok(empleadoplanilla);
        }


        // GET: api/Empleado_planilla/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Empleado_planilla</returns>
        /// <response code="200">Devuelve si el valor es encontrado</response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetEmpleado_planilla")]
        [Route("api/GetEmpleado_planilla")]
        public IHttpActionResult Get(int id)
        {
            var empleadoplanilla = db.Empleados_Planillas
                .Include(l => l.Empleado)
                .Include(l => l.Bono)
                .Include(l => l.Deduccion)
                .Select(l => new
                {
                    l.Id,
                    l.EmpleadoId,
                    l.BonificacionesId,
                    l.Anticipo,
                    l.DeduccionesId,
                    l.Sueldoneto,

                })
                .FirstOrDefault();

            if (empleadoplanilla == null)
                return NotFound();

            return Ok(empleadoplanilla);
        }

        // POST: api/Empleado_planilla

        /// <summary>
        /// Agregar
        /// </summary>
        /// <param name="empleado_planilla"></param>
        /// <returns>JSON Empleado_planilla</returns>
        /// <response code="200">Devuelve si los valores son agregados</response>
        /// <response code="404">Devuelve si no son agregados</response>
        [HttpPost]
        [SwaggerOperation("PostEmpleado_planilla")]
        [Route("api/PostEmpleado_planilla")]
        public IHttpActionResult Post(Empleado_planilla empleado_planilla)
        {
            if (empleado_planilla == null)
                return BadRequest("Empleado inválido.");

            var empleado = db.Empleados.Find(empleado_planilla.EmpleadoId);

            if (empleado == null)
                return BadRequest("Empleado no encontrado.");

            // Buscar la bonificación del mismo empleado
            var bonificacion = db.Bonificaciones
                                 .FirstOrDefault(b => b.EmpleadoId == empleado_planilla.EmpleadoId);

            // Buscar la deducción del mismo empleado
            var deduccion = db.Deducciones
                              .FirstOrDefault(d => d.EmpleadoId == empleado_planilla.EmpleadoId);

            if (bonificacion == null)
                return BadRequest("Bonificación no encontrada para el empleado.");

            if (deduccion == null)
                return BadRequest("Deducción no encontrada para el empleado.");

            empleado_planilla.BonificacionesId = bonificacion.BonificacionesId;
            empleado_planilla.DeduccionesId = deduccion.DeduccionesId;
            empleado_planilla.Sueldoneto = (empleado.Salario - empleado_planilla.Anticipo + bonificacion.Monto  - deduccion.Monto);
            db.Empleados_Planillas.Add(empleado_planilla);
            db.SaveChanges();
            return Ok(empleado_planilla);
        }

        // PUT: api/Empleado_planilla/5

        /// <summary>
        /// Modificar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="epmodificar"></param>
        /// <returns>JSON Empleado_planilla</returns>
        /// <response code="200">Devuelve si los valores son moficados</response>
        /// <response code="404">Devuelve si no son modificados</response>
        [HttpPut]
        [SwaggerOperation("PutEmpleado_planilla")]
        [Route("api/PutEmpleado_planilla")]
        public IHttpActionResult Put(int id,Empleado_planilla epmodificar)
        {
            
            var empleadoplanilla = db.Empleados_Planillas.Find(id);
            if (empleadoplanilla == null)
                return BadRequest("Empleado inválido.");

            var empleado = db.Empleados.Find(epmodificar.EmpleadoId);
            var bonificacion = db.Bonificaciones.Find(epmodificar.BonificacionesId);
            var deduccion = db.Deducciones.Find(epmodificar.DeduccionesId);
            if (bonificacion == null)
                return BadRequest("Bonificacion no encontrada.");

            if (empleado == null)
                return BadRequest("Empleado no encontrado.");
            if (deduccion == null)
                return BadRequest("Empleado no encontrado.");

            empleadoplanilla.EmpleadoId = epmodificar.EmpleadoId;
            empleadoplanilla.BonificacionesId = epmodificar.BonificacionesId;
            empleadoplanilla.Anticipo = epmodificar.Anticipo;
            empleadoplanilla.DeduccionesId = epmodificar.DeduccionesId;

            empleadoplanilla.Sueldoneto = (empleado.Salario - empleadoplanilla.Anticipo + bonificacion.Monto - deduccion.Monto);
            
            db.SaveChanges();
            return Ok(empleadoplanilla);
            
        }

        // DELETE: api/Empleado_planilla/5

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
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
        [HttpGet]
        [Route("api/Empleado_planilla/Ordenadoporsaldototal")]
        public IHttpActionResult Ordenadoporsaldototal()
        {
            var que = from empleado1 in db.Empleados
                      join empleadoplanilla in db.Empleados_Planillas on empleado1.EmpleadoId equals empleadoplanilla.EmpleadoId
                      join bonificacion1 in db.Bonificaciones on empleadoplanilla.BonificacionesId equals bonificacion1.BonificacionesId
                      join deducciones1 in db.Deducciones on empleadoplanilla.BonificacionesId equals deducciones1.DeduccionesId
                      orderby empleadoplanilla.Sueldoneto descending
                      select new
                      {
                          Idempleadoplanilla = empleadoplanilla.Id,
                          Idempleado = empleado1.EmpleadoId,
                          Idbonificacion = bonificacion1.BonificacionesId,
                          cantidad = empleadoplanilla.Anticipo,
                          iddeduccion = deducciones1.DeduccionesId,
                          subtotal = empleadoplanilla.Sueldoneto

                      };
            return Ok(que);
        }
    }
}
