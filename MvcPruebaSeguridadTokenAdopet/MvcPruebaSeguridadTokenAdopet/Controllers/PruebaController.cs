using Microsoft.AspNetCore.Mvc;
using MvcPruebaSeguridadTokenAdopet.Services;
using NuGetAdoPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPruebaSeguridadTokenAdopet.Controllers
{
    public class PruebaController : Controller
    {
        private ServicePruebas service;

        public PruebaController(ServicePruebas service)
        {

            this.service = service;
        }

        public IActionResult LogIn() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string username, string password)
        {

            string token = await this.service.GetToken(username, password);

            if (token == null)
            {

                ViewData["MENSAJE"] = "Usuario/Password incorrectos";

                return View();
            }
            else
            {
                //UNA VEZ QUE TENEMOS EL TOKEN RECUPERAMOS EL PERFIL DEL EMPLEADO Y ALMACENAMOS LOS DATOS DE FORMA PERSONALIZADA
                ViewData["MENSAJE"] = "Bienvenid@";

                VistaCuentas cuenta = await this.service.GetCuentaAsync(token);

                return View(cuenta);
                //return RedirectToAction("Index", "Home");
            }
        }
    }
}
