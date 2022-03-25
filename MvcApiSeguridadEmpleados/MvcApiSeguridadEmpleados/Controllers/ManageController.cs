using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcApiSeguridadEmpleados.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiSeguridadEmpleados.Controllers
{
    public class ManageController : Controller
    {
        private ServiceApiEmpleado service;

        public ManageController(ServiceApiEmpleado service) {

            this.service = service;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string username,int password) {

            string token = await this.service.GetToken(username, password);

            if (token == null)
            {

                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
            }
            else {

                ViewData["MENSAJE"] = "Bienvenid@";
                ViewData["TOKEN"] = token;
                HttpContext.Session.SetString("TOKEN", token);
            }

            return View();
        }
    }
}
