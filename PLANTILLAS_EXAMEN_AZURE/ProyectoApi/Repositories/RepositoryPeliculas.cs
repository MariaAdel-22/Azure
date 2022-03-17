using ApiPeliculas.Data;
using ApiPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repositories
{
    public class RepositoryPeliculas
    {
        private PeliculasContext context;

        public RepositoryPeliculas(PeliculasContext context) {

            this.context = context;
        }

        public List<Pelicula> GetPeliculas() {

            return this.context.Peliculas.ToList();
        }

        public List<Genero> GetGeneros() {

            return this.context.Generos.ToList();
        }

        public List<Nacionalidad> GetNacionalidades() {

            return this.context.Nacionalidades.ToList();
        }

        public Pelicula FindPeliculaId(int idPelicula) {

            return this.context.Peliculas.SingleOrDefault(x => x.IdPelicula == idPelicula);
        }

        public List<Pelicula> FindPeliculasGenero(int idGenero) {

            var consulta = from datos in this.context.Peliculas where datos.IdGenero == idGenero select datos;

            return consulta.ToList();
        }

        public List<Pelicula> FindPeliculasNacionalidad(int idNacionalidad) {

            var consulta = from datos in this.context.Peliculas where datos.IdNacionalidad == idNacionalidad select datos;

            return consulta.ToList();
        }

        public List<Pelicula> FindPeliculasNacGen(int idGenero, int idNacionalidad) {

            var consulta = from datos in this.context.Peliculas where datos.IdGenero == idGenero && datos.IdNacionalidad == idNacionalidad select datos;

            return consulta.ToList();
        }

        private int GetMaxIdPelicula() {

            if (this.context.Peliculas.Count() == 0)
            {

                return 1;
            }
            else {

                var consulta = (from datos in this.context.Peliculas select datos.IdPelicula).Max();

                int idPel = consulta + 1;

                return idPel;
            }
        }

        public void InsertPelicula(int idDistribuidor, int idGenero, string titulo, int idNacionalidad, string argumento, string foto, string fechaEstreno,
            string actores, int duracion, int precio) {

            Pelicula pel = new Pelicula
            {
                IdPelicula = this.GetMaxIdPelicula(),
                IdDistribuidor = idDistribuidor,
                IdGenero = idGenero,
                Titulo = titulo,
                IdNacionalidad = idNacionalidad,
                Argumento = argumento,
                Foto = foto,
                FechaEstreno = fechaEstreno,
                Actores = actores,
                Duracion = duracion,
                Precio = precio
            };

            this.context.Peliculas.Add(pel);
            this.context.SaveChanges();
        }

        public void ModifyPelicula(int idPelicula, int idDistribuidor, int idGenero, string titulo, int idNacionalidad, string argumento, string foto, string fechaEstreno,
            string actores, int duracion, int precio) {

            Pelicula pel = this.FindPeliculaId(idPelicula);

            pel.IdDistribuidor = idDistribuidor;
            pel.IdGenero = idGenero;
            pel.Titulo = titulo;
            pel.IdNacionalidad = idNacionalidad;
            pel.Argumento = argumento;
            pel.Foto = foto;
            pel.FechaEstreno = fechaEstreno;
            pel.Actores = actores;
            pel.Duracion = duracion;
            pel.Precio = precio;

            this.context.SaveChanges();
        }

        public void DeletePelicula(int idPelicula) {

            Pelicula pel = this.FindPeliculaId(idPelicula);

            this.context.Peliculas.Remove(pel);
            this.context.SaveChanges();
        }
    }
}
