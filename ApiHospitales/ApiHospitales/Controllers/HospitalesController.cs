using ApiHospitales.Models;
using ApiHospitales.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHospitales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalesController : ControllerBase
    {
        private RepositoryHospital repo;

        public HospitalesController(RepositoryHospital repo) {

            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Hospital>> GetHospitales() {

            return this.repo.GetHospitales();
        }

        [HttpGet("{id}")]
        public ActionResult<Hospital> GetHospital(int id) {

            return this.repo.FindHospital(id);
        }
    }
}
