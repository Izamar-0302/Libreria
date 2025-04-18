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
    public class Detalle_VentaController : ApiController
    {
        // GET: Detalle_Venta
        private static readonly HttpClient client = new HttpClient();
        [HttpGet]
        public async Task<HttpResponseMessage> Get(DataSourceLoadOptions loadOptions)
        {
            var apiUrl = "https://localhost:44370/api/GetDetalle_Ventas";
            var respuestaJson = await GetAsync(apiUrl);
            //System.Diagnostics.Debug.WriteLine(respuestaJson); imprimir info
            List<Detalle_Venta> listaDetalle_Venta = JsonConvert.DeserializeObject<List<Detalle_Venta>>(respuestaJson);
            return Request.CreateResponse(DataSourceLoader.Load(listaDetalle_Venta, loadOptions));
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

            // Obtener desde la API
            var apiUrlGetDetalle_Venta = $"https://localhost:44370/api/GetDetalle_Venta?id={key}";
            var respuestaDetalle_Venta = await GetAsync(apiUrlGetDetalle_Venta);
            if (string.IsNullOrEmpty(respuestaDetalle_Venta))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El Detalle no fue encontrado.");
            }

            Detalle_Venta Detalle_Venta = JsonConvert.DeserializeObject<Detalle_Venta>(respuestaDetalle_Venta);

            // Asignar los valores del formulario al objeto 
            JsonConvert.PopulateObject(values, Detalle_Venta);

            // Serializar el objeto actualizado
            string jsonString = JsonConvert.SerializeObject(Detalle_Venta);
            var httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            // Realizar la solicitud PUT a la API
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Solicitar J
                var url = $"https://localhost:44370/api/PutDetalle_Venta?id={key}";
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

            var url = "https://localhost:44370/api/PostDetalle_Venta";
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

            var apiUrlDelDetalle_Venta = $"https://localhost:44370/api/DeleteDetalle_Venta?id={key}";
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
                var respuestaEmpleado = await client.DeleteAsync(apiUrlDelDetalle_Venta);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}