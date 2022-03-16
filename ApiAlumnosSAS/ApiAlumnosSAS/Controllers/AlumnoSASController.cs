using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAlumnosSAS.Services;

namespace ApiAlumnosSAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoSASController : ControllerBase
    {
        private ApiAlumnossas service;

        public AlumnoSASController(ApiAlumnossas service) {

            this.service = service;
        }

        [HttpGet]
        [Route("[action]/{curso}")]
        public ActionResult<string> Token(string curso) {

            //tenemos que devolver un string
            //debemos devolver un JSON y no un string PURO

            return Ok(
                new
                {
                    token = this.service.GetToken(curso)
                }); ;
        }
    }
}
