using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using MvcApiSeguridadEmpleados.Models;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MvcApiSeguridadEmpleados.Services
{
    public class ServiceApiEmpleado
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiEmpleado(string urlApi) {

            this.UrlApi = urlApi;
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> GetToken(string username, int password) {

            using (HttpClient client = new HttpClient()) {

                client.BaseAddress = new Uri(this.UrlApi);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                LoginModel model = new LoginModel
                {
                    UserName = username,
                    Password = password
                };

                string json = JsonConvert.SerializeObject(model);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                string request = "/auth/login";

                HttpResponseMessage response = await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    JObject jobject = JObject.Parse(data);

                    string token = jobject.GetValue("response").ToString();

                    return token;
                }
                else {

                    return null;
                }
            }
        }

        //METODOS GENERICOS
        //METODO PARA LAS PETICIOENS SIN SEGURIDAD
        private async Task<T> CallApiAsync<T>(string request) {

            using (HttpClient client = new HttpClient()) {

                client.BaseAddress = new Uri(this.UrlApi);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode) {

                    T data = await response.Content.ReadAsAsync<T>();

                    return data;

                } else {

                    return default(T);
                }
            }
        }

        //METODO CON SEGURIDAD QUE RECIBE EL TOKEN
        private async Task<T> CallApiAsync<T>(string request,string token) {

            using (HttpClient client= new HttpClient()) {

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
                else {

                    return default(T);
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosAsync(string token) {

            string request = "/api/empleados";

            List<Empleado> empleados = await this.CallApiAsync<List<Empleado>>(request,token);

            return empleados;
        }

        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado) {

            string request = "/api/empleados/" + idEmpleado;

            Empleado empleado = await this.CallApiAsync<Empleado>(request);

            return empleado;
        }

        public async Task<Empleado> GetPerfilEmpleado(string token) {

            string request = "/api/empleados/perfilempleado";

            Empleado empleado = await this.CallApiAsync<Empleado>(request,token);

            return empleado;
        }

        public async Task<List<Empleado>> GetCompis(string token) {

            string request = "/api/empleados/compis";

            List<Empleado> empleados = await this.CallApiAsync<List<Empleado>>(request, token);

            return empleados;
        }

        public async Task<List<Empleado>> GetSubordinados(string token) {

            string request = "/api/empleados/subordinados";

            List<Empleado> empleados = await this.CallApiAsync<List<Empleado>>(request, token);

            return empleados;
        }
    }
}
