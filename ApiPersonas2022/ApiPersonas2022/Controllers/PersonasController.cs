using ApiPersonas2022.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPersonas2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        public List<Persona> personas;

        public PersonasController() {

            this.personas = new List<Persona>
            {
                new Persona{ IdPersona = 1, Nombre = "Lucia", Edad = 18},
                new Persona{ IdPersona = 2, Nombre = "Marcos",Edad=99},
                new Persona{ IdPersona=3, Nombre="Pedro",Edad=44},
                new Persona{ IdPersona= 4, Nombre="Carlos",Edad=44}
            };
        }

        [HttpGet]
        public ActionResult<List<Persona>> Get()
        {
            return this.personas;
        }

        [HttpGet("{id}")]
        public ActionResult<Persona> Get(int id) {


            return this.personas.SingleOrDefault(x => x.IdPersona == id);
        }


    }
}
