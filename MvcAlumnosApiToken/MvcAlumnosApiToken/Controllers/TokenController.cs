using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcAlumnosApiToken.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAlumnosApiToken.Controllers
{
    public class TokenController : Controller
    {
        private ServiceApiToken service;

        public TokenController(ServiceApiToken service) {

            this.service = service;
        }

        public IActionResult GenerarToken()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerarToken(string curso) {

            string token = await this.service.GetTokenAsync(curso);

            //ALMACENAMOS EN SESSION EL TOKEN GENERADO
            HttpContext.Session.SetString("TOKEN", token);
            ViewData["TOKEN"] = token;

            return View();
        }
    }
}
