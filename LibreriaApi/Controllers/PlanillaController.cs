using LibreriaApi.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LibreriaApi.Controllers
{
    public class PlanillaController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Planilla
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Planilla</returns>
        /// <response code="200">Devuelve al obtener los valore</response>
        /// <response code="404">Devueleve si no obtuvo los valores</response>
        [HttpGet]
        [SwaggerOperation("GetPlanillas")]
        [Route("api/GetPlanillas")]
        public IEnumerable<Planilla> Get()
        {
            return db.Planillas;
        }

        // GET: api/Planilla/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Planilla</returns>
        /// <response code="200">Devueleve si encuentra valor</response>
        /// <response code="404">Devuelve si no encuentra valor</response>
        [HttpGet]
        [SwaggerOperation("GetPlanilla")]
        [Route("api/GetPlanilla")]
        public IHttpActionResult Get(int id)
        {
            Planilla planilla = db.Planillas.Find(id);
            if (planilla == null)
            {
                return NotFound();
            }
            return Ok(planilla);
        }

        // POST: api/Planilla
        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <param name="planilla"></param>
        /// <returns>JSON Planilla</returns>
        /// <response code="200">Devuelve cuando se agrega valor</response>
        /// <respone code= "404">Devueleve cuando no se agrega</respone>
        [HttpPost]
        [SwaggerOperation("PostPlanilla")]
        [Route("api/PostPlanilla")]
        public IHttpActionResult Post(Planilla planilla)
        {
            db.Planillas.Add(planilla);
            db.SaveChanges();
            return Ok(planilla);
        }

        // PUT: api/Planilla/5
        /// <summary>
        /// Modificar valor
        /// </summary>
        /// <param name="planillamodificado"></param>
        /// <returns>JSON Planilla</returns>
        /// <responde code="200">Devuelve cuando se ha modificado el valor</responde>
        /// <response code="404">Devuelve si no se ha modificado</response>
        [HttpPut]
        [SwaggerOperation("PutPlanilla")]
        [Route("api/PutPlanilla")]
        public IHttpActionResult Put(Planilla planillamodificado)
        {
            int id = planillamodificado.Id;
            db.Entry(planillamodificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(planillamodificado);
        }

        // DELETE: api/Planilla/5
        /// <summary>
        /// Eliminar valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Planilla</returns>
        /// <response code="200">Devuelve al eliminar valor</response>
        /// <response code="404">Devuelve al no eliminarlo</response>
        [HttpPut]
        [SwaggerOperation("PutPlanilla")]
        [Route("api/PutPlanilla")]
        public IHttpActionResult Delete(int id)
        {
            Planilla planilla = db.Planillas.Find(id);
            db.Planillas.Remove(planilla);
            db.SaveChanges();
            return Ok(planilla);
        }
    }
}
