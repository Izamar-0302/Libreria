using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DevExtremeLibreria.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace DevExtremeLibreria.Controllers
{
    public class PlanillaController : ApiController
    {
        private static readonly HttpClient client = new HttpClient();
        [HttpGet]
        public async Task<HttpResponseMessage> Get(DataSourceLoadOptions loadOptions)
        {
            var apiUrl = "https://localhost:44370/api/GetPlanillas";
            var respuestaJson = await GetAsync(apiUrl);
            //System.Diagnostics.Debug.WriteLine(respuestaJson); imprimir info
            List<Planilla> listaPlanilla = JsonConvert.DeserializeObject<List<Planilla>>(respuestaJson);
            return Request.CreateResponse(DataSourceLoader.Load(listaPlanilla, loadOptions));
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
            var apiUrlGetPlanilla = $"https://localhost:44370/api/GetPlanilla?id={key}";
            var respuestaPlanilla = await GetAsync(apiUrlGetPlanilla);
            if (string.IsNullOrEmpty(respuestaPlanilla))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "La Planilla no fue encontrado.");
            }

            Planilla Planilla = JsonConvert.DeserializeObject<Planilla>(respuestaPlanilla);

            // Asignar los valores del formulario al objeto autor
            JsonConvert.PopulateObject(values, Planilla);

            // Serializar el objeto actualizado
            string jsonString = JsonConvert.SerializeObject(Planilla);
            var httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            // Realizar la solicitud PUT a la API
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
<<<<<<< Updated upstream
                var url = $"https://localhost:44370/api/PutPlanilla?id={key}";
=======
                var url = "https://localhost:44370/api/PutPlanilla/" + key;
>>>>>>> Stashed changes
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

            var url = "https://localhost:44370/api/PostPlanilla";
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

            var apiUrlDelaplanilla = $"https://localhost:44370/api/DeletePlanilla?id={key}" ;
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
                var respuestaPlanilla = await client.DeleteAsync(apiUrlDelaplanilla);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}