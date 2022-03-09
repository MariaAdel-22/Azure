using Microsoft.AspNetCore.Mvc;
using MvcApiDoctoresRoutes.Services;
using NuGetDoctores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiDoctoresRoutes.Controllers
{
    public class DoctoresController : Controller
    {
        private ServiceApiDoctores service;

        public DoctoresController(ServiceApiDoctores service) {

            this.service = service;
        }

        public async Task<IActionResult> DoctoresEspecialidad()
        {
            List<string> especialidades = await this.service.GetEspecialidadesAsync();

            List<Doctor> doctores = await this.service.GetDoctoresAsync();

            ViewData["ESPECIALIDADES"] = especialidades;

            return View(doctores);
        }

        [HttpPost]
        public async Task<IActionResult> DoctoresEspecialidad(string especialidad) {

            List<string> especialidades = await this.service.GetEspecialidadesAsync();

            List<Doctor> doctores = await this.service.GetDoctoresEspecialidadAsync(especialidad);

            ViewData["ESPECIALIDADES"] = especialidades;

            return View(doctores);
        }
    }
}
