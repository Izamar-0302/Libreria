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
    public class UbicacionController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Ubicacion
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Ubicacion</returns>
        /// <response code="200">Devuelve al obtener los valore</response>
        /// <response code="404">Devueleve si no obtuvo los valores</response>
        [HttpGet]
        [SwaggerOperation("GetUbicaciones")]
        [Route("api/GetUbicaciones")]
        public IEnumerable<Ubicacion> Get()
        {
            return db.Ubicaciones;
        }



        // GET: api/Ubicacion/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Ubicacion</returns>
        /// <response code="200">Devuelve si encuentra valor</response>
        /// <response code="404">Devuelve si no encuentra valor</response>
        [HttpGet]
        [SwaggerOperation("GetUbicacion")]
        [Route("api/GetUbicacion")]
        public IHttpActionResult Get(int id)
        {
            Ubicacion ubicacion = db.Ubicaciones.Find(id);
            if (ubicacion == null)
            {
                return NotFound();
            }
            return Ok(ubicacion);
        }
        

        // POST: api/Ubicacion
        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <param name="ubicacion"></param>
        /// <returns>JSON Ubicacion</returns>
        /// <response code="200">Devuelve cuando se agrega valor</response>
        /// <respone code= "404">Devueleve cuando no se agrega</respone>
        [HttpPost]
        [SwaggerOperation("PostUbicacion")]
        [Route("api/PostUbicacion")]
        public IHttpActionResult Post(Ubicacion ubicacion)
        {
            db.Ubicaciones.Add(ubicacion);
            db.SaveChanges();
            return Ok(ubicacion);
        }

        // PUT: api/Ubicacion/5
        /// <summary>
        /// Modificar valor
        /// </summary>
        /// <param name="ubicacionmodificado"></param>
        /// <returns>JSON Ubicacion</returns>
        /// <responde code="200">Devuelve cuando se ha modificado el valor</responde>
        /// <response code="404">Devuelve si no se ha modificado</response>
        public IHttpActionResult Put(Ubicacion ubicacionmodificado)
        {
            int id = ubicacionmodificado.UbicacionId;
            db.Entry(ubicacionmodificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(ubicacionmodificado);
        }

        // DELETE: api/Ubicacion/5
        /// <summary>
        /// Eliminar valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Ubicacion</returns>
        /// <response code="200">Devuelve al eliminar valor</response>
        /// <response code="404">Devuelve al no eliminarlo</response>
        public IHttpActionResult Delete(int id)
        {
            Ubicacion ubicacion = db.Ubicaciones.Find(id);
            db.Ubicaciones.Remove(ubicacion);
            db.SaveChanges();
            return Ok(ubicacion);
        }
    }
}
