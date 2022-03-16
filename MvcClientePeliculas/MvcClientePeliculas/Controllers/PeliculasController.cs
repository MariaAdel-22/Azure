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

            //TempData["GENEROS"] = generos;

            HttpContext.Session.SetString("GENEROS",JsonConvert.SerializeObject(generos));

            List<Nacionalidad> nacionalidades = await this.service.GetNacionalidadesAsync();

            TempData["NACIONALIDADES"] = nacionalidades;

            return View();
        }
    }
}
