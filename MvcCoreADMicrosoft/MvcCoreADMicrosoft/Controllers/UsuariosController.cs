using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreADMicrosoft.Controllers
{
    public class UsuariosController : Controller
    {
        [Authorize]
        public IActionResult Perfil()
        {
            return View();
        }
    }
}
