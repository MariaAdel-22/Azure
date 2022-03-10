using ApiCrudDoctores2022.Models;
using ApiCrudDoctores2022.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudDoctores2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private RepositoryDoctor repo;

        public DoctorController(RepositoryDoctor repo) {

            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Doctor>> GetDoctores() {

            return this.repo.GetDoctores();
        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> FindDoctor(string id) {

            return this.repo.FindDoctor(id);
        }

        [HttpPost]
        public ActionResult InsertarDoctor(Doctor doctor) {

            this.repo.CrearDoctor(doctor.IdHospital, doctor.Apellido, doctor.Especialidad, doctor.Salario);

            return Ok();
        }

        [HttpPut]
        public ActionResult ModificarDoctor(Doctor doctor) {

            this.repo.ModificarDoctor(doctor.IdDoctor, doctor.IdHospital, doctor.Apellido, doctor.Especialidad, doctor.Salario);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult EliminarDoctor(string id) {

            this.repo.EliminarDoctor(id);

            return Ok();
        }

        //VAMOS A MODIFICAR EL SALARIO DE LOS DOCTORES POR ESPECIALIDAD
        [HttpPut]
        [Route("[action]/{especialidad}/{incremento}")]
        public void UpdateSalarioDoctores(string especialidad, int incremento) {

            this.repo.UpdateSalarioDoctores(especialidad, incremento);
        }

        [HttpGet]
        [Route("[action]")]

        public ActionResult<List<string>> Especialidades() {

            return this.repo.GetEspecialidades();
        }

        [HttpGet]
        [Route("[action]/{especialidad}")]
        public ActionResult<List<Doctor>> DoctoresEspecialidad(string especialidad) {

            return this.repo.GetDoctoresEspecialidad(especialidad);
        }

        //doctoresespecialidades?especialidad=pediatria&especialidad=cardiologia
        [HttpGet]
        [Route("[action]")]
        public ActionResult<List<Doctor>> DoctoresEspecialidades([FromQuery] List<string>especialidad) {

            return this.repo.GetDoctoresEspecialidades(especialidad);
        }
    }
}
