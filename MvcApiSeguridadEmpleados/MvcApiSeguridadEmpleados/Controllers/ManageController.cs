using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcApiSeguridadEmpleados.Models;
using MvcApiSeguridadEmpleados.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

                return View();
            }
            else {
                //UNA VEZ QUE TENEMOS EL TOKEN RECUPERAMOS EL PERFIL DEL EMPLEADO Y ALMACENAMOS LOS DATOS DE FORMA PERSONALIZADA
                ViewData["MENSAJE"] = "Bienvenid@";

                Empleado empleado = await this.service.GetPerfilEmpleado(token);

                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);

                identity.AddClaim(new Claim(ClaimTypes.Name,empleado.Apellido));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier,empleado.IdEmpleado.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, empleado.Oficio));

                identity.AddClaim(new Claim("TOKEN",token));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties
                    {

                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(36)
                    });

                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> LogOut() {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("TOKEN");

            return RedirectToAction("Index", "Home");
        }
    }
}
