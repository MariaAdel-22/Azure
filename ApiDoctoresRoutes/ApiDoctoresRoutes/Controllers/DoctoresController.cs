using ApiDoctoresRoutes.Models;
using ApiDoctoresRoutes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDoctoresRoutes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctoresController : ControllerBase
    {
        private RepositoryDoctor repo;

        public DoctoresController(RepositoryDoctor repo) {

            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Doctor>> GetDoctores() {

            return this.repo.GetDoctores();
        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> GetDoctor(string id) {

            return this.repo.GetDoctor(id);
        }
    }
}
