using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcPruebaMetodosApiAdoPetToken.Services;
using Newtonsoft.Json;
using NuGetAdoPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcPruebaMetodosApiAdoPetToken.Controllers
{
    public class PruebaController : Controller
    {
        private ServiceAlumno service;

        public PruebaController(ServiceAlumno service)
        {

            this.service = service;
        }

        public IActionResult LogIn()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string nombre, string pass)
        {
            string token = await this.service.GetTokenAsync(nombre, pass);

            if (token == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
            else
            {

                VistaCuentas cuenta =await this.service.BuscarCuenta(token);

                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme
                    , ClaimTypes.Name, ClaimTypes.Role);

                identity.AddClaim(new Claim("TOKEN", token));
                identity.AddClaim(new Claim("CUENTA", JsonConvert.SerializeObject(cuenta)));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , principal, new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    });
                return RedirectToAction("GetProtectoras");
            }

        }

        public async Task<IActionResult> GetProtectoras() {

            string token = HttpContext.User.FindFirst("TOKEN").Value;
            VistaCuentas cuenta=JsonConvert.DeserializeObject<VistaCuentas>(HttpContext.User.FindFirst("CUENTA").Value);

            List<Protectora> protectoras = new List<Protectora>();

            if (cuenta.usuario != null) {

                protectoras = await this.service.GetProtectoras(token, cuenta.usuario.Ciudad);

            } else if (cuenta.protectora != null) {

                protectoras = await this.service.GetProtectoras(token, cuenta.protectora.Ciudad);
            }

            return View(protectoras);
        }

        public async Task<IActionResult> AnimalesProtectora() {

            string token = HttpContext.User.FindFirst("TOKEN").Value;
            VistaCuentas cuenta = JsonConvert.DeserializeObject<VistaCuentas>(HttpContext.User.FindFirst("CUENTA").Value);

            List<Animal> animales;

            if (cuenta.protectora != null)
            {

                animales = await this.service.GetAnimalesProtectora(cuenta.protectora.IdProtectora, token);
            }
            else {

                animales = new List<Animal>();
            }
            

            return View(animales);
        }

        public IActionResult InsertarAnimal() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertarAnimal(string Nombre,string Edad,string Especie,string Genero,IFormFile Imagen,string Peso) {

            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.InsertarAnimal(Nombre,Edad,Especie,Genero,Imagen.FileName,Peso,token);

            return RedirectToAction("AnimalesProtectora");
        }
    }
}
