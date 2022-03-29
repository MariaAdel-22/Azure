using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcClienteAlumnoPractica.Filters;
using MvcClienteAlumnoPractica.Models;
using MvcClienteAlumnoPractica.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcClienteAlumnoPractica.Controllers
{
    public class AlumnoController : Controller
    {
        private ServiceAlumno service;

        public AlumnoController(ServiceAlumno service) {

            this.service = service;
        }


        public IActionResult LogIn() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string nombre, string apellidos)
        {
            string token =await this.service.GetTokenAsync(nombre, apellidos);

            if (token == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
            else
            {

                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme
                    , ClaimTypes.Name, ClaimTypes.Role);

                identity.AddClaim(new Claim("nombre", nombre));
                identity.AddClaim(new Claim("apellidos", apellidos));
                identity.AddClaim(new Claim("TOKEN", token));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , principal, new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    });
                return RedirectToAction("Index");
            }

        }

        [AuthorizeAlumno]
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;

            List<Alumno> alumnos = await this.service.GetAlumnosAsync(token);

            return View(alumnos);
        }

        [AuthorizeAlumno]
        public async Task<IActionResult> Detalles(int id) {

            string token = HttpContext.User.FindFirst("TOKEN").Value;

            Alumno alumno = await this.service.Detalles(id,token);

            return View(alumno);
        }

        public async Task<IActionResult> Editar(int id) {

            string token = HttpContext.User.FindFirst("TOKEN").Value;

            Alumno alumno = await this.service.Detalles(id,token);

            return View(alumno);
        }

        [HttpPost]
        [AuthorizeAlumno]
        public async Task<IActionResult> Editar(Alumno alumno) {

            string token = HttpContext.User.FindFirst("TOKEN").Value;

            await this.service.Editar(alumno.idAlumno, alumno.Curso, alumno.Nombre, alumno.Apellidos, alumno.Nota, token);

            return RedirectToAction("Index");
        }

        [AuthorizeAlumno]
        public async Task<IActionResult> Delete(int id) {

            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.DeleteAlumno(id,token);

            return RedirectToAction("Index");
        }

        [AuthorizeAlumno]
        public IActionResult Insertar() {

            return View();
        }

        [HttpPost]
        [AuthorizeAlumno]
        public async Task<IActionResult> Insertar(Alumno alumno,IFormFile imagen) {

            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.Insertar(alumno.idAlumno, alumno.Curso, alumno.Nombre, alumno.Apellidos, alumno.Nota, token);
            await this.service.InsertarImagenBlob(imagen);
            return RedirectToAction("Index");
        }
    }
}
