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
    public class SucursalController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Sucursal
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Sucursal</returns>
        /// <response code="200">Devuelve al obtener los valore</response>
        /// <response code="404">Devueleve si no obtuvo los valores</response>
        [HttpGet]
        [SwaggerOperation("GetSucursales")]
        [Route("api/GetSucursales")]
        public IEnumerable<Sucursal> Get()
        {
            return db.Sucursales;
        }

        // GET: api/Sucursal/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Sucursal</returns>
        /// <response code="200">Devueleve si encuentra valor</response>
        /// <response code="404">Devuelve si no encuentra valor</response>
        [HttpGet]
        [SwaggerOperation("GetSucursal")]
        [Route("api/GetSucursal")]
        public IHttpActionResult Get(int id)
        {
            Sucursal sucursal = db.Sucursales.Find(id);
            if (sucursal == null)
            {
                return NotFound();
            }
            return Ok(sucursal);
        }

        // POST: api/Sucursal
        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <param name="sucursal"></param>
        /// <returns>JSON Sucursal</returns>
        /// <response code="200">Devuelve cuando se agrega valor</response>
        /// <respone code= "404">Devueleve cuando no se agrega</respone>
        [HttpPost]
        [SwaggerOperation("PostSucursal")]
        [Route("api/PostSucursal")]
        public IHttpActionResult Post(Sucursal sucursal)
        {
            db.Sucursales.Add(sucursal);
            db.SaveChanges();
            return Ok(sucursal);
        }

        // PUT: api/Sucursal/5
        /// <summary>
        /// Modificar valor
        /// </summary>
        /// <param name="sucursalmodificado"></param>
        /// <returns>JSON Sucursal</returns>
        /// <responde code="200">Devuelve cuando se ha modificado el valor</responde>
        /// <response code="404">Devuelve si no se ha modificado</response>
        [HttpPut]
        [SwaggerOperation("PutSucursal")]
        [Route("api/PutSucursal")]
        public IHttpActionResult Put(Sucursal sucursalmodificado)
        {
            int id = sucursalmodificado.SucursalId;
            db.Entry(sucursalmodificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(sucursalmodificado);
        }

        // DELETE: api/Sucursal/5
        /// <summary>
        /// Eliminar valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Sucursal</returns>
        /// <response code="200">Devuelve al eliminar valor</response>
        /// <response code="404">Devuelve al no eliminarlo</response>
        [HttpDelete]
        [SwaggerOperation("DeleteSucursal")]
        [Route("api/DeleteSucursal")]
        public IHttpActionResult Delete(int id)
        {
            Sucursal sucursal = db.Sucursales.Find(id);
            db.Sucursales.Remove(sucursal);
            db.SaveChanges();
            return Ok(sucursal);
        }
    }
}
