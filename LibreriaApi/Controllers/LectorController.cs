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
    public class LectorController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Lector
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Lector</returns>
        /// <response code="200">Devuelve al obtener los valore</response>
        /// <response code="404">Devueleve si no obtuvo los valores</response>
        [HttpGet]
        [SwaggerOperation("GetLectores")]
        [Route("api/GetLectores")]
        public IEnumerable<Lector> Get()
        {
            return db.Lectores;
        }

        // GET: api/Lector/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Lector</returns>
        /// <response code="200">Devueleve si encuentra valor</response>
        /// <response code="404">Devuelve si no encuentra valor</response>
        [HttpGet]
        [SwaggerOperation("GetLector")]
        [Route("api/GetLector")]
        public IHttpActionResult Get(int id)
        {
            Lector lector = db.Lectores.Find(id);
            if (lector == null)
            {
                return NotFound();
            }
            return Ok(lector);
        }

        // POST: api/Lector
        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <param name="lector"></param>
        /// <returns>JSON Lector</returns>
        /// <response code="200">Devuelve cuando se agrega valor</response>
        /// <respone code= "404">Devueleve cuando no se agrega</respone>
        [HttpPost]
        [SwaggerOperation("PostLector")]
        [Route("api/PostLector")]
        public IHttpActionResult Post(Lector lector)
        {
            db.Lectores.Add(lector);
            db.SaveChanges();
            return Ok(lector);
        }

        // PUT: api/Lector/5
        /// <summary>
        /// Modificar valor
        /// </summary>
        /// <param name="lectormodificado"></param>
        /// <returns>JSON Editorial</returns>
        /// <responde code="200">Devuelve cuando se ha modificado el valor</responde>
        /// <response code="404">Devuelve si no se ha modificado</response>
        public IHttpActionResult Put(Editorial lectormodificado)
        {
            int id = lectormodificado.Id;
            db.Entry(lectormodificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(lectormodificado);
        }

        // DELETE: api/Lector/5
        /// <summary>
        /// Eliminar valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Lector</returns>
        /// <response code="200">Devuelve al eliminar valor</response>
        /// <response code="404">Devuelve al no eliminarlo</response>
        public IHttpActionResult Delete(int id)
        {
            Lector lector = db.Lectores.Find(id);
            db.Lectores.Remove(lector);
            db.SaveChanges();
            return Ok(lector);
        }


    }
}
