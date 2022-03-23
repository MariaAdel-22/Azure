using Microsoft.AspNetCore.Mvc;
using MvcCocheCosmosDb.Models;
using MvcCocheCosmosDb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCocheCosmosDb.Controllers
{
    public class VehiculosController : Controller
    {
        private ServiceCocheCosmos service;

        public VehiculosController(ServiceCocheCosmos service) {

            this.service = service;
        }

        public IActionResult Index() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string accion) {

            await this.service.CreateDatabaseAsync();

            ViewData["MENSAJE"] = "BBDD y Container generados";

            return View();
        }

        public async Task<IActionResult> Coches()
        {
            List<Vehiculo> coches = await this.service.GetVehiculosAsync();

            return View(coches);
        }

        public IActionResult Create() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehiculo car,string existemotor) {

            await this.service.AddVehiculoAsync(car);

            return RedirectToAction("Coches");
        }

        public async Task<IActionResult> Details(string id) {

            Vehiculo car = await this.service.FindVehiculoAsync(id);

            return View(car);
        }

        public async Task<ActionResult> Edit(string id) {

            Vehiculo car = await this.service.FindVehiculoAsync(id);

            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vehiculo car) {

            await this.service.UpdateVehiculoAsync(car);
            return RedirectToAction("Coches");
        }

        public async Task<IActionResult> Delete(string id) {

            await this.service.DeleteVehiculoAsync(id);
            return RedirectToAction("Coches");
        }


        public ActionResult BuscarMarca()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BuscarMarca(string marca)
        {

            List<Vehiculo> cars = await this.service.BuscarVehiculosMarca(marca);

            return View(cars);
        }

       
    }
}
