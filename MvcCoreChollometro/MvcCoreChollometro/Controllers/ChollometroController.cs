using Microsoft.AspNetCore.Mvc;
using MvcCoreChollometro.Models;
using MvcCoreChollometro.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreChollometro.Controllers
{
    public class ChollometroController : Controller
    {
        private RepositoryChollometro repo;

        public ChollometroController(RepositoryChollometro repo) {

            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Chollo> chollos = this.repo.GetChollos();
            ViewData["NUMERO"] = chollos.Count;

            return View(chollos);
        }
    }
}
