using Microsoft.AspNetCore.Mvc;
using MvcComicsMysql.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcComicsMysql.Controllers
{
    public class ComicsController : Controller
    {
        private RepositoryComics repo;

        public ComicsController(RepositoryComics repo) {

            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View(this.repo.GetComics());
        }
    }
}
