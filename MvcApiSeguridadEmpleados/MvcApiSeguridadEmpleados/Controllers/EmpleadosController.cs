﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcApiSeguridadEmpleados.Models;
using MvcApiSeguridadEmpleados.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiSeguridadEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private ServiceApiEmpleado service;

        public EmpleadosController(ServiceApiEmpleado service) {

            this.service = service;
        }

        public async Task<IActionResult> Empleados()
        {
            string token = HttpContext.Session.GetString("TOKEN");

            if (token == null)
            {

                ViewData["MENSAJE"] = "No tiene permisos";
                return View();
            }
            else {

                List<Empleado> empleados = await this.service.GetEmpleadosAsync(token);

                return View(empleados);
            }

        }

        public async Task<IActionResult> Details(int id) {

            Empleado empleado = await this.service.FindEmpleadoAsync(id);

            return View(empleado);
        }
    }
}
