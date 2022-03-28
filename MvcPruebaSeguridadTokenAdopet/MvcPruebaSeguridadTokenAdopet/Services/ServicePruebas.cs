using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGetAdoPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcPruebaSeguridadTokenAdopet.Services
{
    public class ServicePruebas
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServicePruebas(string urlApi)
        {

            this.UrlApi = urlApi;
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> GetToken(string username, string password)
        {

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(this.UrlApi);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                LoginModel model = new LoginModel
                {
                    Nombre = username,
                    Password = password
                };

                string json = JsonConvert.SerializeObject(model);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                string request = "/api/Inicio/ValidarCuenta";

                HttpResponseMessage response = await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    JObject jobject = JObject.Parse(data);

                    string token = jobject.GetValue("response").ToString();

                    return token;
                }
                else
                {

                    return null;
                }
            }
        }

        //METODOS GENERICOS
        //METODO PARA LAS PETICIOENS SIN SEGURIDAD
        private async Task<T> CallApiAsync<T>(string request)
        {

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(this.UrlApi);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {

                    T data = await response.Content.ReadAsAsync<T>();

                    return data;

                }
                else
                {

                    return default(T);
                }
            }
        }

        //METODO CON SEGURIDAD QUE RECIBE EL TOKEN
        private async Task<T> CallApiAsync<T>(string request, string token)
        {

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(this.UrlApi);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {

                    T data = await response.Content.ReadAsAsync<T>();

                    return data;
                }
                else
                {

                    return default(T);
                }
            }
        }

        public async Task<VistaCuentas> GetCuentaAsync(string token)
        {

            string request = "/api/inicio/buscarcuenta";

           VistaCuentas cuenta = await this.CallApiAsync<VistaCuentas>(request, token);

            return cuenta;
        }
    }
}
