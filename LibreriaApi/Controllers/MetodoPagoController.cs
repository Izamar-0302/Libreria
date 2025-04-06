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
    public class MetodoPagoController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/MetodoPago
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON MetodoPago</returns>
        /// <response code="200">Devuelve al obtener los valore</response>
        /// <response code="404">Devueleve si no obtuvo los valores</response>
        [HttpGet]
        [SwaggerOperation("GetMetodosPago")]
        [Route("api/GetMetodosPago")]
        public IEnumerable<MetodoPago> Get()
        {
            return db.MetodosPagos;
        }

        // GET: api/MetodoPago/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON MetodoPago</returns>
        /// <response code="200">Devueleve si encuentra valor</response>
        /// <response code="404">Devuelve si no encuentra valor</response>
        [HttpGet]
        [SwaggerOperation("GetMetodoPago")]
        [Route("api/GetMetodoPago")]
        public IHttpActionResult Get(int id)
        {
            MetodoPago metodoPago = db.MetodosPagos.Find(id);
            if (metodoPago == null)
            {
                return NotFound();
            }
            return Ok(metodoPago);
        }

        // POST: api/MetodoPago
        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <param name="metodoPago"></param>
        /// <returns>JSON MetodoPago</returns>
        /// <response code="200">Devuelve cuando se agrega valor</response>
        /// <respone code= "404">Devueleve cuando no se agrega</respone>
        [HttpPost]
        [SwaggerOperation("PostMetodoPago")]
        [Route("api/MetodoPago")]
        public IHttpActionResult Post(MetodoPago metodoPago)
        {
            db.MetodosPagos.Add(metodoPago);
            db.SaveChanges();
            return Ok(metodoPago);
        }

        // PUT: api/MetodoPago/5
        /// <summary>
        /// Modificar valor
        /// </summary>
        /// <param name="metodoPagomodificado"></param>
        /// <returns>JSON MetodoPago</returns>
        /// <responde code="200">Devuelve cuando se ha modificado el valor</responde>
        /// <response code="404">Devuelve si no se ha modificado</response>
        [HttpPut]
        [SwaggerOperation("PutMetodoPago")]
        [Route("api/PutMetodoPago")]
        public IHttpActionResult Put(MetodoPago metodoPagomodificado)
        {
            int id = metodoPagomodificado.Id;
            db.Entry(metodoPagomodificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(metodoPagomodificado);
        }

        // DELETE: api/MetodoPago/5
        /// <summary>
        /// Eliminar valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON MetodoPago</returns>
        /// <response code="200">Devuelve al eliminar valor</response>
        /// <response code="404">Devuelve al no eliminarlo</response>
        [HttpDelete]
        [SwaggerOperation("DeleteMetodoPago")]
        [Route("api/DeleteMetodoPago")]
        public IHttpActionResult Delete(int id)
        {
            MetodoPago metodoPago = db.MetodosPagos.Find(id);
            db.MetodosPagos.Remove(metodoPago);
            db.SaveChanges();
            return Ok(metodoPago);
        }
    }
}
