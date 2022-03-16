using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using MvcClientePeliculas.Models;

namespace MvcClientePeliculas.Services
{
    public class ServiceApiPeliculas
    {
        private Uri UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiPeliculas(string url) {

            this.UrlApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request) {

            using (HttpClient client = new HttpClient()) {

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

        public async Task<List<Pelicula>> GetPeliculasAsync() {

            string request = "/api/peliculas";

            List<Pelicula> peliculas = await this.CallApiAsync<List<Pelicula>>(request);

            return peliculas;
        }

        public async Task<List<Genero>> GetGenerosAsync() {

            string request = "/api/generos";

            List<Genero> generos = await this.CallApiAsync<List<Genero>>(request);

            return generos;
        }

        public async Task<List<Nacionalidad>> GetNacionalidadesAsync() {

            string request = "/api/nacionalidades";

            List<Nacionalidad> nacionalidades = await this.CallApiAsync<List<Nacionalidad>>(request);

            return nacionalidades;
        }
    }
}
