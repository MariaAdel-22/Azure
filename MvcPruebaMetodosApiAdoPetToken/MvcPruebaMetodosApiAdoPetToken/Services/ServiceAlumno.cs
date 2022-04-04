using Microsoft.AspNetCore.Http;
using MvcPruebaMetodosApiAdoPetToken.Models;
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

namespace MvcPruebaMetodosApiAdoPetToken.Services
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
                    Password=apellidos
                };
                string json = JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                string request = "/api/inicio/validarcuenta";
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

        public async Task<VistaCuentas> BuscarCuenta(string token) {

            string request = "/api/inicio/buscarcuenta";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                HttpResponseMessage response =await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string cuen = await response.Content.ReadAsStringAsync();

                    VistaCuentas cuenta = JsonConvert.DeserializeObject<VistaCuentas>(cuen);

                    return cuenta;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<Protectora>> GetProtectoras(string token,string ciudad) {

            string request = "/api/protectoras/getprotectoras/"+ciudad;

           List<Protectora>protectoras=await this.CallApiAsync<List<Protectora>>(request, token);

            return protectoras;
        }

        public async Task<List<Animal>> GetAnimalesProtectora(int codigoProtectora,string token) {

            string request = "/api/protectoras/getanimalesprotectora/" + codigoProtectora;

            List<Animal> anim = await this.CallApiAsync<List<Animal>>(request, token);

            return anim;
        }

        public async Task InsertarAnimal(string nombre,string edad,string especie,string genero,string imagen,string peso,string token) {

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                string request = "/api/protectoras/insertaranimal";

                Animal an = new Animal { Nombre=nombre, Edad = edad, Especie = especie, Genero = genero, Imagen = imagen, Peso = peso };

                string json = JsonConvert.SerializeObject(an);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }
    }
}
