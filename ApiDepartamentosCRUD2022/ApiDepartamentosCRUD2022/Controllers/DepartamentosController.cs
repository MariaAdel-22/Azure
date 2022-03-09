using ApiDepartamentosCRUD2022.Models;
using ApiDepartamentosCRUD2022.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDepartamentosCRUD2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private RepositoryDepartamento repo;

        public DepartamentosController(RepositoryDepartamento repo) {

            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Departamento>> GetDepartamentos() {

            return this.repo.GetDepartamentos();
        }

        [HttpGet("{id}")]
        public ActionResult<Departamento> GetDepartamento(int id) {

            return this.repo.FindDepartamento(id);
        }

        //EN LAS CONSULTAS DE ACCION POST Y PUT SE RECIBEN LOS OBJETOS POR DEFECTO
        //PODRIAMOS HACER QUE NO DEVOLVIERA NADA Y LA RESPUESTA ES 200 SI TODO VA BIEN O TAMBIEN PODRIAMOS PERSONALIZAR LA RESPUESTA
       
        [HttpPost]
        public ActionResult InsertDepartamento(Departamento departamento) {

            this.repo.InsertarDepartamento(departamento.IdDepartamento, departamento.Nombre, departamento.Localidad);

            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateDepartamento(Departamento departamento) {

            this.repo.UpdateDepartamento(departamento.IdDepartamento,departamento.Nombre,departamento.Localidad);

            return Ok();
        }

        //EL METODO DELETE POR DEFECTO RECIBE {ID}
        [HttpDelete("{id}")]
        public void DeleteDepartamento(int id) {

            this.repo.DeleteDepartamento(id);
        }
    }
}
