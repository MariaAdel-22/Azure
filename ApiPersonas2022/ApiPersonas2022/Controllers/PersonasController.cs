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
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Mi primer API";
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id) {

            return "Primer Api Get ID";
        }
    }
}
