using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcPruebaSeguridadTokenAdopet.Filters;
using MvcPruebaSeguridadTokenAdopet.Services;
using NuGetAdoPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,
                   ClaimTypes.Name, ClaimTypes.Role);

                identity.AddClaim(new Claim("TOKEN", token));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(36)
                    });

                //return View(cuenta);
                return RedirectToAction("Perfil");
            }
        }

        [AuthorizeCuentas]
        public async Task<IActionResult> Perfil() {

            string token = HttpContext.User.FindFirst("TOKEN").Value;

            VistaCuentas cuenta = await this.service.GetCuentaAsync(token);

            return View(cuenta);
        }
    }
}
