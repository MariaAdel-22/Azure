using ApiPeliculas.Models;
using ApiPeliculas.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {

        private RepositoryPeliculas repo;

        public PeliculasController(RepositoryPeliculas repo) {

            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Pelicula>> GetPeliculas() {

            return this.repo.GetPeliculas();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult<Pelicula> FindPeliculaId(int id) {

            return this.repo.FindPeliculaId(id);
        }

        [HttpGet]
        [Route("[action]/{idGenero}")]
        public ActionResult<List<Pelicula>> FindPeliculasGenero(int idGenero) {

            return this.repo.FindPeliculasGenero(idGenero);
        }

        [HttpGet]
        [Route("[action]/{idNacionalidad}")]
        public ActionResult<List<Pelicula>> FindPeliculasNacionalidad(int idNacionalidad) {

            return this.repo.FindPeliculasNacionalidad(idNacionalidad);
        }

        [HttpGet]
        [Route("[action]/{idgenero}/{idnacionalidad}")]
        public ActionResult<List<Pelicula>> FindPeliculasGeneroNacionalidad(int idgenero, int idnacionalidad) {

            return this.repo.FindPeliculasNacGen(idgenero, idnacionalidad);
        }

        [HttpPost]
        public ActionResult InsertPelicula(Pelicula pelicula) {

            this.repo.InsertPelicula(pelicula.IdDistribuidor,pelicula.IdGenero,pelicula.Titulo,pelicula.IdNacionalidad,
                pelicula.Argumento,pelicula.Foto,pelicula.FechaEstreno,pelicula.Actores,pelicula.Duracion,pelicula.Precio);

            return Ok();
        }

        [HttpPut]
        public ActionResult ModificarPelicula(Pelicula pelicula) {

            this.repo.ModifyPelicula(pelicula.IdPelicula, pelicula.IdDistribuidor, pelicula.IdGenero, pelicula.Titulo, pelicula.IdNacionalidad,
                pelicula.Argumento, pelicula.Foto, pelicula.FechaEstreno, pelicula.Actores, pelicula.Duracion, pelicula.Precio);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult EliminarPelicula(int idPelicula) {

            this.repo.DeletePelicula(idPelicula);

            return Ok();
        }
    }
}
