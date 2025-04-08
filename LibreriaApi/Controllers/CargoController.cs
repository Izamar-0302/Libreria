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
    public class CargoController : ApiController
    {

        private DBContextProyect db = new DBContextProyect();
        // GET: api/Cargo
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Cargo</returns>
        /// <response code="200">Devuelve si encontro los valores</response>
        /// <response code="404">Devuelve si no los encuentra</response>
        [HttpGet]
        [SwaggerOperation("GetCargos")]
        [Route("api/GetCargos")]
        public IEnumerable<Cargo> Get()
        {
            return db.Cargo;
        }

        // GET: api/Cargo/5
        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Cargo</returns>
        /// <response code="200">Devuelve si el valor es encontrado </response>
        /// <response code="404">Devuelve si no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetCargo")]
        [Route("api/GetCargo")]
        public IHttpActionResult Get(int id)
        {
            Cargo cargo = db.Cargo.Find(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return Ok(cargo);
        }

        // POST: api/Cargo
        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <param name="cargo"></param>
        /// <returns>JSON Cargo</returns>
        /// <response code="200">Devuelve si se agrega el valor</response>
        /// <response code="404">Devueleve si no se agrego valor</response>
        [HttpPost]
        [SwaggerOperation("PostCargo")]
        [Route("api/PostCargo")]
        public IHttpActionResult Post(Cargo cargo)
        {
            db.Cargo.Add(cargo);
            db.SaveChanges();
            return Ok(cargo);
        }

        /// <summary>
        /// Modificar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cargomodificado"></param>
        /// <returns>JSON Cargo</returns>
        /// <response code="200">Devuelve al modificar valor</response>
        /// <response code="404">Devuelve si no se modifico valor</response>

        [HttpPut]
        [SwaggerOperation("PutCargo")]
        [Route("api/PutCargo")]
        public IHttpActionResult Put(int id,Cargo cargomodificado)
        {
           Cargo cargo = db.Cargo.Find(id);
            if (cargo == null)
            {
                return NotFound();
            }

            cargo.Titulo = cargomodificado.Titulo;
            cargo.Descripcion= cargomodificado.Descripcion;
            cargo.Departamento = cargomodificado.Departamento;
            cargo.CargoId= id;
            db.SaveChanges();
            return Ok(cargo);
        }

        // DELETE: api/Cargo/5
        /// <summary>
        /// Borrar valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Cargo</returns>
        /// <response code="200">Devuelve al modificar valor</response>
        /// <response code="404">Devuelve si no se modifico valor</response>
        public IHttpActionResult Delete(int id)
        {
            Cargo cargo = db.Cargo.Find(id);
            db.Cargo.Remove(cargo);
            db.SaveChanges();
            return Ok(cargo);
        }
    }
}
