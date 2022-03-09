using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using NuGetDoctores;
using System.Net.Http;

namespace MvcApiDoctoresRoutes.Services
{
    public class ServiceApiDoctores
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiDoctores(string url) {

            this.UrlApi = url;
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        //Hacemos un método genérico para las peticiones
        private async Task<T> CallApiAsync<T>(string request) {

            using (HttpClient client = new HttpClient()) {

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

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

        public async Task<List<Doctor>> GetDoctoresAsync() {

            string request = "/api/doctores";

            List<Doctor> doctores = await CallApiAsync<List<Doctor>>(request);

            return doctores;
        }

        public async Task<List<string>> GetEspecialidadesAsync() {

            string request = "/api/doctores/especialidades";

            List<string> especialidades = await CallApiAsync<List<string>>(request);

            return especialidades;
        }

        public async Task<List<Doctor>> GetDoctoresEspecialidadAsync(string especialidad) {

            string request = "/api/doctores/doctoresespecialidad/" + especialidad;

            List<Doctor> doctores = await CallApiAsync<List<Doctor>>(request);

            return doctores;
        }
    }
}
