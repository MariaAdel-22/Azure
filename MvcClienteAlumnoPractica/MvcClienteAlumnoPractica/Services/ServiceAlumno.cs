using Microsoft.AspNetCore.Http;
using MvcClienteAlumnoPractica.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcClienteAlumnoPractica.Services
{
    public class ServiceAlumno
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceAlumno(string urlapi)
        {
            this.UrlApi = urlapi;
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> GetTokenAsync(string nombre, string apellidos)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                LoginModel model = new LoginModel
                {
                    Nombre=nombre,
                    Apellidos=apellidos
                };
                string json = JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                string request = "/api/alumno/validaralumno";
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    JObject jObject = JObject.Parse(data);
                    string token = jObject.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
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

        private async Task<T> CallApiAsync<T>(string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
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

        public async Task<List<Alumno>>
            GetAlumnosAsync(string token)
        {
            string request = "/api/alumno";
            List<Alumno> alumnos =
                await this.CallApiAsync<List<Alumno>>(request, token);
            return alumnos;
        }

        public async Task<Alumno> Detalles(int idalumno, string token) {

            string request = "/api/alumno/detalles/"+idalumno;

            Alumno al = await this.CallApiAsync<Alumno>(request,token);

            return al;
        }


        public async Task Insertar(int idalumno, string curso, string nombre, string apellidos, int nota, string token) {


            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                string request = "/api/alumno/insertaralumno";

                Alumno al = new Alumno { idAlumno = idalumno, Curso = curso, Nombre = nombre, Apellidos = apellidos, Nota = nota };

                string json = JsonConvert.SerializeObject(al);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(request, content);   
            }

        }

        public async Task Editar(int idalumno,string curso, string nombre, string apellidos,int nota,string token)
        {

            using (HttpClient client = new HttpClient()) {

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                string request = "/api/alumno";

                Alumno al = await this.Detalles(idalumno, token);

                if (al != null)
                {

                    al.Curso = curso;
                    al.Nombre = nombre;
                    al.Apellidos = apellidos;
                    al.Nota = nota;

                    string json = JsonConvert.SerializeObject(al);

                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(request, content);

                }
            }      
        }


        public async Task DeleteAlumno(int idalumno,string token) {

            string request = "api/alumno/" + idalumno;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }

        }

        public async Task InsertarImagenBlob(IFormFile imagen)
        {
            string UrlBlobImagen = "https://prod-86.westeurope.logic.azure.com:443/workflows/7298bea22b53479982425ab2d75bb577/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=a3J4pFEsJFZcjyJD1ZfSFLGZBDJhovvpJ9nbhWqMwkc";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                var blobModel = new
                {
                    imagen=imagen
                };

                var json = JsonConvert.SerializeObject(blobModel);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync(UrlBlobImagen, content);

                if (response.IsSuccessStatusCode)
                {
                    await response.Content.ReadAsFormDataAsync();

                }
            }
        }
    }
}
