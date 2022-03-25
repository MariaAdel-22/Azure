using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcApiSeguridadEmpleados.Filters;
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

        [AuthorizeEmpleados]
        public async Task<IActionResult> Empleados()
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;

            List<Empleado> empleados = await this.service.GetEmpleadosAsync(token);

            return View(empleados);
        }

        [AuthorizeEmpleados]
        public async Task<IActionResult> Perfil() {

            string token = HttpContext.User.FindFirst("TOKEN").Value;

            Empleado emp = await this.service.GetPerfilEmpleado(token);

            return View(emp);
        }

        [AuthorizeEmpleados]
        public async Task<IActionResult> Compis() {

            string token = HttpContext.User.FindFirst("TOKEN").Value;

            List<Empleado> compis = await this.service.GetCompis(token);

            return View(compis);
        }

        [AuthorizeEmpleados]
        public async Task<IActionResult> Subordinados() {

            string token = HttpContext.User.FindFirst("TOKEN").Value;

            List<Empleado> empleados = await this.service.GetSubordinados(token);

            return View(empleados);
        }

        public async Task<IActionResult> Details(int id) {

            Empleado empleado = await this.service.FindEmpleadoAsync(id);

            return View(empleado);
        }
    }
}
