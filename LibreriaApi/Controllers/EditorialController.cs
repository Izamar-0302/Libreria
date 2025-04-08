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
    public class EditorialController : ApiController
    {
        private DBContextProyect db = new DBContextProyect();
        // GET: api/Editorial
        /// <summary>
        /// Obtener valores
        /// </summary>
        /// <returns>JSON Editorial</returns>
        /// <response code="200">Devuelve al obtener los valore</response>
        /// <response code="404">Devueleve si no obtuvo los valores</response>
        [HttpGet]
        [SwaggerOperation("GetEditoriales")]
        [Route("api/GetEditoriales")]
        public IEnumerable<Editorial> Get()
        {
            return db.Editoriales;
        }

        // GET: api/Editorial/5

        /// <summary>
        /// Obtener un valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Editorial</returns>
        /// <response code="200">Devueleve si encuentra valor</response>
        /// <response code="404">Devuelve si no encuentra valor</response>
        [HttpGet]
        [SwaggerOperation("GetEditorial")]
        [Route("api/GetEditorial")]
        public IHttpActionResult Get(int id)
        {
            Editorial editorial = db.Editoriales.Find(id);
            if (editorial == null)
            {
                return NotFound();
            }
            return Ok(editorial);
        }


        // POST: api/Editorial
        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <param name="editorial"></param>
        /// <returns>JSON Editorial</returns>
        /// <response code="200">Devuelve cuando se agrega valor</response>
        /// <respone code= "404">Devueleve cuando no se agrega</respone>
        [HttpPost]
        [SwaggerOperation("PostEditorial")]
        [Route("api/PostEditorial")]
        public IHttpActionResult Post(Editorial editorial)
        {
            db.Editoriales.Add(editorial);
            db.SaveChanges();
            return Ok(editorial);
        }

        // PUT: api/Editorial/5
        /// <summary>
        /// Modificar valor
        /// </summary>
        /// <param name="editorialmodificado"></param>
        /// <returns>JSON Editorial</returns>
        /// <responde code="200">Devuelve cuando se ha modificado el valor</responde>
        /// <response code="404">Devuelve si no se ha modificado</response>

        [HttpPut]
        [SwaggerOperation("PutEditorial")]
        [Route("api/PutEditorial/{id}")]
        public IHttpActionResult Put(int id, Editorial editorialmodificado)
        {
            Editorial editorial = db.Editoriales.Find(id);
            if (editorial == null)
            {
                return NotFound();
            }

            editorial.Nombre =editorialmodificado.Nombre;
            editorial.Pais = editorialmodificado.Nombre;
            editorial.Fundacion = editorialmodificado.Fundacion;
            editorial.Catalogo = editorialmodificado.Catalogo;

            db.SaveChanges();

            return Ok(editorial);
        }
        // DELETE: api/Editorial/5
        /// <summary>
        /// Eliminar valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Editorial</returns>
        /// <response code="200">Devuelve al eliminar valor</response>
        /// <response code="404">Devuelve al no eliminarlo</response>
        [HttpDelete]
        [SwaggerOperation("DeleteEditorial")]
        [Route("api/DeleteEditorial/{id}")]
        public IHttpActionResult Delete(int id)
        {
            Editorial editorial = db.Editoriales.Find(id);
            if (editorial == null)
            {
                return NotFound();
            }
            db.Editoriales.Remove(editorial);
            db.SaveChanges();
            return Ok(editorial);
        }
    }
}
