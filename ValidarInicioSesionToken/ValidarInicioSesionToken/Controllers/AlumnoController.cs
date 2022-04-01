using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidarInicioSesionToken.Filters;
using ValidarInicioSesionToken.Models;
using ValidarInicioSesionToken.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NuGetAdoPet.Models;

namespace ValidarInicioSesionToken.Controllers
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

            VistaCuentas alumnos = await this.service.GetAlumnosAsync(token);

            return View(alumnos);
        }

        
    }
}
