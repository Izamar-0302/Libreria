using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
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
    public class PedidoController : ApiController
    {
        // GET: Pedido
        private static readonly HttpClient client = new HttpClient();
        [HttpGet]
        public async Task<HttpResponseMessage> Get(DataSourceLoadOptions loadOptions)
        {
            var apiUrl = "https://localhost:44370/api/GetPedidos";
            var respuestaJson = await GetAsync(apiUrl);
            //System.Diagnostics.Debug.WriteLine(respuestaJson); imprimir info
            List<Pedido> listaPedido = JsonConvert.DeserializeObject<List<Pedido>>(respuestaJson);
            return Request.CreateResponse(DataSourceLoader.Load(listaPedido, loadOptions));
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

            // Obtener  desde la API
            var apiUrlGetPedido = $"https://localhost:44370/api/GetPedido?id={key}";
            var respuestaPedido = await GetAsync(apiUrlGetPedido);
            if (string.IsNullOrEmpty(respuestaPedido))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El Pedido no fue encontrado.");
            }

            Pedido Pedido = JsonConvert.DeserializeObject<Pedido>(respuestaPedido);

            // Asignar los valores del formulario al objeto autor
            JsonConvert.PopulateObject(values, Pedido);

            // Serializar el objeto actualizado
            string jsonString = JsonConvert.SerializeObject(Pedido);
            var httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            // Realizar la solicitud PUT a la API
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
                var url = $"https://localhost:44370/api/PutPedido?id={key}";
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

            var url = "https://localhost:44370/api/PostPedido ";
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

            var apiUrlDelPedido = $"https://localhost:44370/api/DeletePedido?id={key}";
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
                var respuestaPedido = await client.DeleteAsync(apiUrlDelPedido);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}