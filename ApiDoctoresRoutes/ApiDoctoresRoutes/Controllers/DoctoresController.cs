using NuGetDoctores;
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

        //api/doctores/especialidades
        [HttpGet]
        [Route("[action]")]//con esto le decimos que tenga el mismo nombre que el del ActionResult
        public ActionResult<List<string>> Especialidades() {

            return this.repo.GetEspecialidades();
        }

        //api/doctores/doctoresespecialidad/cardiologia
        [HttpGet]
        [Route("[action]/{especialidad}")]
        public ActionResult<List<Doctor>> DoctoresEspecialidad(string especialidad) {

            return this.repo.GetDoctoresEspecialidades(especialidad);
        }

        //api/doctores/555/cardiologia
        [HttpGet]
        [Route("{salario}/{especialidad}")]
        public ActionResult<List<Doctor>> DoctoresSalario(int salario,string especialidad) {

            return this.repo.GetDoctores(salario, especialidad);
        }
    }
}
