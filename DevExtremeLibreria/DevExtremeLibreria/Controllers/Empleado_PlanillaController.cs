using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DevExtremeLibreria.Models;
using Newtonsoft.Json;
using System.Web.Http;

namespace DevExtremeLibreria.Controllers
{
    public class Empleado_PlanillaController : ApiController
    {
        // GET: Empleado_Planilla
        private static readonly HttpClient client = new HttpClient();
        [HttpGet]
        public async Task<HttpResponseMessage> Get(DataSourceLoadOptions loadOptions)
        {
            var apiUrl = "https://localhost:44370/api/GetEmpleado_planillas";
            var respuestaJson = await GetAsync(apiUrl);
            //System.Diagnostics.Debug.WriteLine(respuestaJson); imprimir info
            List<Empleado_planilla> listaEmpleado_planillas = JsonConvert.DeserializeObject<List<Empleado_planilla>>(respuestaJson);
            return Request.CreateResponse(DataSourceLoader.Load(listaEmpleado_planillas, loadOptions));
        }

        public static async Task<string> GetAsync(string uri)
        {
            try
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                using (var client = new HttpClient(handler))
                {
                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }

            }
            catch (Exception e)
            {
                var m = e.Message;
                return null;
            }

        }
        [HttpPut]
        public async Task<HttpResponseMessage> Put(FormDataCollection form)
        {
            // Obtener los parámetros del formulario
            var key = Convert.ToInt32(form.Get("key")); // llave que estoy modificando
            var values = form.Get("values"); // Los valores modificados en formato JSON

            // Obtener el autor desde la API
            var apiUrlGetEmpleado_planilla = $"https://localhost:44370/api/GetEmpleado_planilla?id={key}";
            var respuestaEmpleado_planilla = await GetAsync(apiUrlGetEmpleado_planilla);
            if (string.IsNullOrEmpty(respuestaEmpleado_planilla))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Planilla no fue encontrada.");
            }

            Empleado_planilla Empleado_planilla = JsonConvert.DeserializeObject<Empleado_planilla>(respuestaEmpleado_planilla);

            // Asignar los valores del formulario al objeto 
            JsonConvert.PopulateObject(values, Empleado_planilla);

            // Serializar el objeto actualizado
            string jsonString = JsonConvert.SerializeObject(Empleado_planilla);
            var httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            // Realizar la solicitud PUT a la API
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Solicitar J
                var url = $"https://localhost:44370/api/PutEmpleado_planilla?id={key}";
                var response = await client.PutAsync(url, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return Request.CreateErrorResponse(response.StatusCode, error);
                }

                var result = await response.Content.ReadAsStringAsync();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


        [HttpPost]
        public async Task<HttpResponseMessage> Post(FormDataCollection form)
        {

            var values = form.Get("values");

            var httpContent = new StringContent(values, System.Text.Encoding.UTF8, "application/json");

            var url = "https://localhost:44370/api/PostEmpleado_planilla";
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
                var response = await client.PostAsync(url, httpContent);

                var result = response.Content.ReadAsStringAsync().Result;
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));

            var apiUrlDelEmpleado_planilla = $"https://localhost:44370/api/DeleteEmpleado_planilla?id={key}";
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
                var respuestaEmpleado = await client.DeleteAsync(apiUrlDelEmpleado_planilla);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}