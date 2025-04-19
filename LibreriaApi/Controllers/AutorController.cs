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
    public class AutorController : ApiController
    {
        private DBContextProyect bd = new DBContextProyect();

        /// GET: api/Autor
        /// <summary>
        /// Obtener todos los autores
        /// </summary>
        /// <returns>JSON Autor</returns>
        /// <response code="200">Devuelve los valores si fueron encontrados</response>
        /// <responde code="404">Si no se ha encontrado</responde>
        [HttpGet]
        [SwaggerOperation("GetAutores")]
        [Route("api/GetAutores")]
        public IEnumerable<Autor> Get()
        {
            return bd.Autores;
        }

        /// GET: api/Autor/5
        /// <summary>
        /// Obtener un solo valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Autor</returns>
        /// <response code="200">Devuelve si el valor ha sido encontrado</response>
        /// <response code="404">Si no ha sido encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetAutor")]
        [Route("api/GetAutor")]
        public IHttpActionResult Get(int id)
        {
            Autor autor = bd.Autores.Find(id);
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        /// POST: api/Autor
        /// <summary>
        /// Agregar valor
        /// </summary>
        /// <param name="autor"></param>
        /// <returns>JSON Autor</returns>
        /// <response code="200">Devuelve si es agregado el valor</response>
        /// <response  code="404">Devuelve si no se ha agregado el valor</response>
        [HttpPost]
        [SwaggerOperation("PostAutor")]
        [Route("api/PostAutor")]
        public IHttpActionResult Post(Autor autor)
        {
            bd.Autores.Add(autor);
            bd.SaveChanges();
            return Ok(autor);
        }

        /// <summary>
        /// Modificar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autormodificado"></param>
        /// <returns>JSON Autor</returns>
        /// <response code="200">Devuelve si se ha agregado el valor</response>
        /// <response code="404">Devuelve si no se agregado el valor</response>
        [HttpPut]
        [SwaggerOperation("PutAutor")]
        [Route("api/PutAutor/{id}")]
        public IHttpActionResult Put(int id, Autor autormodificado)
        {
            Autor autor = bd.Autores.Find(id);
            if (autor == null)
            {
                return NotFound();
            }

            autor.Nombre = autormodificado.Nombre;
            autor.Nacionalidad = autormodificado.Nacionalidad;
            autor.Fechadenacimiento = autormodificado.Fechadenacimiento;
            autor.Biografia = autormodificado.Biografia;
            bd.SaveChanges(); 
            return Ok(autor); 
        }
        /// DELETE: api/Autor/5
        /// <summary>
        /// Se elimina valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON Autor</returns>
        /// <response code="200">Devuelve si se elimina valor</response>
        /// <response code="404">Devuelve si no se elimina valor </response>
        [HttpDelete]
        [SwaggerOperation("DeleteAutor")]
        [Route("api/DeleteAutor/{id}")]
        public IHttpActionResult Delete(int id)
        {
            Autor autor = bd.Autores.Find(id);
            if (autor == null)
            {
                return NotFound();
            }
            bd.Autores.Remove(autor);
            bd.SaveChanges();
            return Ok(autor);
        }
    }
}
