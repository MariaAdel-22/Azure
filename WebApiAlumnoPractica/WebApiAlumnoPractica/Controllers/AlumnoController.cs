using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiAlumnoPractica.Helpers;
using WebApiAlumnoPractica.Models;
using WebApiAlumnoPractica.Repositories;

namespace WebApiAlumnoPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private RepositoryAlumno repo;
        private HelperAlumnoToken helper;

        public AlumnoController(RepositoryAlumno repo, HelperAlumnoToken helper) {

            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult ValidarAlumno(LoginModel model) {

            Alumno alumno = this.repo.FindAlumno(model.Nombre,model.Apellidos);

            if (alumno == null)
            {
                return Unauthorized();
            }
            else
            {

                SigningCredentials credentials =
                    new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);

                string jsonAlumno = JsonConvert.SerializeObject(alumno);
                Claim[] claims = new[]
                {
                new Claim("UserData", jsonAlumno)
                };


                JwtSecurityToken token =
                    new JwtSecurityToken(
                        claims:claims,
                        issuer: this.helper.Issuer,
                        audience: this.helper.Audience,
                        signingCredentials: credentials,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        notBefore: DateTime.UtcNow
                        );

                return Ok(
                    new
                    {
                        response =
                        new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public ActionResult InsertarAlumno(Alumno al) {

            List<Claim> claims = HttpContext.User.Claims.ToList();

            string jsonAlumno =claims.SingleOrDefault(z => z.Type == "UserData").Value;

            Alumno alumno = JsonConvert.DeserializeObject<Alumno>(jsonAlumno);

            if (alumno.Nombre == "Nacho" || alumno.Nombre == "Adrian" || alumno.Nombre == "Marcos")
            {

                this.repo.InsertarAlumno(al.Curso, al.Nombre, al.Apellidos, al.Nota);

                return Ok();
            }
            else {

                if (al.Curso == alumno.Curso)
                {

                    this.repo.InsertarAlumno(al.Curso, al.Nombre, al.Apellidos, al.Nota);

                    return Ok();
                }
                else {

                    return Unauthorized();
                }
            }
        }

        [HttpPut]
        [Authorize]
        public ActionResult ModificarAlumno(Alumno al) {

            List<Claim> claims = HttpContext.User.Claims.ToList();

            string jsonAlumno = claims.SingleOrDefault(z => z.Type == "UserData").Value;

            Alumno alumno = JsonConvert.DeserializeObject<Alumno>(jsonAlumno);

            if (alumno.Nombre == "Nacho" || alumno.Nombre == "Adrian" || alumno.Nombre == "Marcos")
            {
                this.repo.ModificarAlumno(al.IdAlumno, al.Curso, al.Nombre, al.Apellidos, al.Nota);

                return Ok();
            }
         
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult EliminarAlumno(int id) {

            List<Claim> claims = HttpContext.User.Claims.ToList();

            string jsonAlumno = claims.SingleOrDefault(z => z.Type == "UserData").Value;

            Alumno alumno = JsonConvert.DeserializeObject<Alumno>(jsonAlumno);

            if (alumno.Nombre == "Nacho" || alumno.Nombre == "Adrian" || alumno.Nombre == "Marcos")
            {
                this.repo.DeleteAlumno(id);

                return Ok();
            }

            return Ok();
        }


        [HttpGet]
        [Authorize]
        [Route("[action]/{id}")]
        public ActionResult<Alumno> Detalles(int id) {

            return this.repo.FinIdAlumno(id);
        }


        [HttpGet]
        [Authorize]
        public ActionResult<List<Alumno>> GetAlumnos()
        {

            List<Claim> claims = HttpContext.User.Claims.ToList();

            string jsonAlumno = claims.SingleOrDefault(z => z.Type == "UserData").Value;

            Alumno alumno = JsonConvert.DeserializeObject<Alumno>(jsonAlumno);

            if (alumno.Nombre == "Nacho" || alumno.Nombre == "Adrian" || alumno.Nombre == "Marcos")
            {

                return this.repo.GetAlumnos();
            }
            else
            {

                return this.repo.GetAlumnosCurso(alumno.Curso);
            }
        }
    }
}

