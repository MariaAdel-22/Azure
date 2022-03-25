using ApiEmpleadosOAuth.Models;
using ApiEmpleadosOAuth.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiEmpleadosOAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo) {

            this.repo = repo;
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Empleado> EliminarEmpleado(int id) {

            List<Claim> claims = HttpContext.User.Claims.ToList();

            string json = claims.SingleOrDefault(x => x.Type == "UserData").Value;

            Empleado emp = JsonConvert.DeserializeObject<Empleado>(json);

            Empleado emp2 = this.repo.FindEmpleado(emp.IdEmpleado);

            if (emp.Oficio.ToUpper() == "DIRECTOR" || emp.Oficio.ToUpper() == "PRESIDENTE")
            {

                this.repo.EliminarEmpleado(id);

                return Ok();
            }
            else {

                return Problem("Su oficio no es valido para poder eliminar un empleado");
            }
        }


        [HttpGet]
        [Authorize]
        [Route("[action]/{iddirector}")]
        public ActionResult<List<Empleado>> Subordinados(int iddirector) {

            List<Claim> claims = HttpContext.User.Claims.ToList();

            string json = claims.SingleOrDefault(x => x.Type == "UserData").Value;

            Empleado empleado = JsonConvert.DeserializeObject<Empleado>(json);
            List<Empleado> subordinados = this.repo.GetSubordinados(empleado.IdEmpleado);

            return subordinados;
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public ActionResult<List<Empleado>> Compis() {

            List<Claim> claims = HttpContext.User.Claims.ToList();

            string json = claims.SingleOrDefault(z => z.Type == "UserData").Value;

            Empleado emp = JsonConvert.DeserializeObject<Empleado>(json);

            List<Empleado> empleados = this.repo.GetCompisCurro(emp.IdDepartamento);

            return empleados;
        }


        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public ActionResult<Empleado> PerfilEmpleado() {

            //AQUI HEMOS RECIBIDO EL TOKEN. CUANDO RECIBIMOS EL TOKEN SE MONTA EL SERVICIO Y ESTAMOS DENTRO DE USER
            //ESTAMOS RECUPERANDO LOS CLAIMS DEL TOKEN
            List<Claim> claims = HttpContext.User.Claims.ToList();

            //RECUPERAMOS LA KEY USERDATA QUE ES LA INFORMACION DEL EMPLEADO EN FORMATO JSON
            string jsonempleado = claims.SingleOrDefault(z => z.Type == "UserData").Value;

            Empleado empleado = JsonConvert.DeserializeObject<Empleado>(jsonempleado);

            return empleado;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<Empleado>> GetEmpleados() {

            return this.repo.GetEmpleados();
        }

        [HttpGet("{id}")]
        public ActionResult<Empleado> FindEmpleado(int id) {

            return this.repo.FindEmpleado(id);
        }
    }
}
