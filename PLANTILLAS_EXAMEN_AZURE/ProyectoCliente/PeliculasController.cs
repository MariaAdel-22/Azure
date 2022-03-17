using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcClientePeliculas.Models;
using MvcClientePeliculas.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClientePeliculas.Controllers
{
    public class PeliculasController : Controller
    {
        private ServiceApiPeliculas service;


        public PeliculasController(ServiceApiPeliculas service) {

            this.service = service;
        }


        public async Task<IActionResult> Index()
        {
            List<Genero> generos = await this.service.GetGenerosAsync();

            HttpContext.Session.SetString("GENEROS", JsonConvert.SerializeObject(generos));

            //TempData["GENEROS"] = generos;



            /*List<Nacionalidad> nacionalidades = await this.service.GetNacionalidadesAsync();

            TempData["NACIONALIDADES"] = nacionalidades;*/

            List<Pelicula> peliculas = await this.service.GetPeliculasAsync();

            //await this._CargarGeneros();

            return View(peliculas);
        }


        public async Task<IActionResult> _CargarGeneros() {

            List<Genero> generos = await this.service.GetGenerosAsync();

            HttpContext.Session.SetString("GENEROS", JsonConvert.SerializeObject(generos));

            return PartialView("_CargarGeneros");
        }

        public async Task<IActionResult> modificarPelicula(int idPelicula) {

            Pelicula pel = await this.service.FindPeliculaAsync(idPelicula);

            return View(pel);
        }

        [HttpPost]
        public async Task<IActionResult> modificarPelicula(Pelicula pelicula) {

            await this.service.ModificarPelicula(pelicula.IdPelicula,pelicula.IdDistribuidor,pelicula.IdGenero,pelicula.Titulo,
                pelicula.IdNacionalidad,pelicula.Argumento,pelicula.Foto,pelicula.FechaEstreno,pelicula.Actores,pelicula.Duracion,pelicula.Precio);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> eliminarPelicula(int idPelicula) {

            Pelicula pel = await this.service.FindPeliculaAsync(idPelicula);

            if (pel != null) {

                await this.service.EliminarPeliculaAsync(idPelicula);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> crearPelicula() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> crearPelicula(Pelicula pelicula) {

            await this.service.InsertarPeliculaAsync(pelicula.IdPelicula, pelicula.IdDistribuidor, pelicula.IdGenero, pelicula.Titulo,
                pelicula.IdNacionalidad, pelicula.Argumento, pelicula.Foto, pelicula.FechaEstreno, pelicula.Actores, pelicula.Duracion, pelicula.Precio);

            return RedirectToAction("Index");
        }

        public IActionResult IndexJquery() {

            return View();
        }

        public async Task<IActionResult> ModificarPeliculasJquery(int idPel) {

            Pelicula pel = await this.service.FindPeliculaAsync(idPel);

            return View(pel);
        }

        public async Task<IActionResult> InsertarPeliculaJquery() {

            return View();
        }
    }
}
