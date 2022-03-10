using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using MvcClienteCrudDoctores2022.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcClienteCrudDoctores2022.Services
{
    public class ServiceApiDoctores
    {
        private Uri UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiDoctores(string url) {

            this.UrlApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request) {

            using (HttpClient client= new HttpClient()) {

                client.BaseAddress = this.UrlApi;

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

            string request = "/api/doctor/";

            List<Doctor> doctores = await this.CallApiAsync<List<Doctor>>(request);

            return doctores;
        }

        public async Task<Doctor> FindDoctorAsync(string id) {

            string request = "/api/doctor" + id;

            Doctor doc = await this.CallApiAsync<Doctor>(request);

            return doc;
        }

        public async Task InsertarDoctor(string iddoctor, string idhospital, string apellido,string especialidad,int salario) {

            string request = "/api/doctor";

            using (HttpClient client = new HttpClient()) {

                client.BaseAddress = this.UrlApi;

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Doctor doc = new Doctor { IdDoctor = iddoctor, IdHospital = idhospital, Apellido = apellido, Especialidad = especialidad, Salario = salario };

                string json = JsonConvert.SerializeObject(doc);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task ModificarDoctor(string iddoctor, string idhospital, string apellido, string especialidad, int salario) {

            string request = "/api/doctor";

            using (HttpClient client = new HttpClient()) {

                client.BaseAddress = this.UrlApi;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Doctor doc = new Doctor { IdDoctor = iddoctor, IdHospital = idhospital, Apellido = apellido, Especialidad = especialidad, Salario = salario };

                string json = JsonConvert.SerializeObject(doc);

                StringContent content = new StringContent(json,Encoding.UTF8,"application/json");

                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task EliminarDoctor(string id) {

            string request = "/api/doctor/" + id;

            using (HttpClient client = new HttpClient()) {

                client.BaseAddress = this.UrlApi;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
    }
}
