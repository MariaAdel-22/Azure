using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using MvcClientePeliculas.Models;
using Newtonsoft.Json;
using System.Text;

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

            string request = "/api/Peliculas/Generos";

            List<Genero> generos = await this.CallApiAsync<List<Genero>>(request);

            return generos;
        }

        public async Task<List<Nacionalidad>> GetNacionalidadesAsync() {

            string request = "/api/peliculas/nacionalidades";

            List<Nacionalidad> nacionalidades = await this.CallApiAsync<List<Nacionalidad>>(request);

            return nacionalidades;
        }


        public async Task<Pelicula> FindPeliculaAsync(int id) {

            string request = "/api/Peliculas/FindPeliculaId/" + id;

            Pelicula pel = await this.CallApiAsync<Pelicula>(request);

            return pel;
        }

        public async Task ModificarPelicula(int IdPelicula, int IdDistribuidor, int IdGenero, string Titulo, int IdNacionalidad,
            string Argumento, string Foto, string FechaEstreno, string Actores, int Duracion, int Precio)
        {

            using (HttpClient client= new HttpClient()) {

                string request = "/api/Peliculas";

                client.BaseAddress = this.UrlApi;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Pelicula pel = await this.FindPeliculaAsync(IdPelicula);

                if (pel != null) {

                    pel.IdDistribuidor = IdDistribuidor;
                    pel.IdGenero = IdGenero;
                    pel.Titulo = Titulo;
                    pel.IdNacionalidad = IdNacionalidad;
                    pel.Argumento = Argumento;
                    pel.Foto = Foto;
                    pel.FechaEstreno = FechaEstreno;
                    pel.Actores = Actores;
                    pel.Duracion = Duracion;
                    pel.Precio = Precio;
                }
                
                string json = JsonConvert.SerializeObject(pel);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage message = await client.PutAsync(request, content);
            }
                
        }

        public async Task InsertarPeliculaAsync(int IdPelicula, int IdDistribuidor, int IdGenero, string Titulo, int IdNacionalidad,
            string Argumento, string Foto, string FechaEstreno, string Actores, int Duracion, int Precio) {

            using (HttpClient client= new HttpClient()) {

                string request = "/api/Peliculas";

                client.BaseAddress = this.UrlApi;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Pelicula pel = new Pelicula{

                    IdPelicula = IdPelicula,
                    IdDistribuidor=IdDistribuidor,
                    IdGenero=IdGenero,
                    Titulo=Titulo,
                    IdNacionalidad=IdNacionalidad,
                    Argumento=Argumento,
                    Foto=Foto,
                    FechaEstreno=FechaEstreno,
                    Actores=Actores,
                    Duracion=Duracion,
                    Precio=Precio
                };

                string json = JsonConvert.SerializeObject(pel);

                StringContent content = new StringContent(json,Encoding.UTF8,"application/json");

                HttpResponseMessage message = await client.PostAsync(request,content);
            }
        }

        public async Task EliminarPeliculaAsync(int idPelicula) {

            using (HttpClient client= new HttpClient()) {

                string request = "/api/Peliculas/" + idPelicula;

                client.BaseAddress = this.UrlApi;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                HttpResponseMessage mesage = await client.DeleteAsync(request);
            }
                
        }
    }
}
