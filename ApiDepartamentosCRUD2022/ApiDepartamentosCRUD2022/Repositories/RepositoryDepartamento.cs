using ApiDepartamentosCRUD2022.Data;
using ApiDepartamentosCRUD2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDepartamentosCRUD2022.Repositories
{
    public class RepositoryDepartamento
    {
        public DepartamentosContext context;

        public RepositoryDepartamento(DepartamentosContext context) {

            this.context = context;
        }

        public List<Departamento> GetDepartamentos() {

            return this.context.Departamentos.ToList();
        }

        public Departamento FindDepartamento(int id) {

            return this.context.Departamentos.FirstOrDefault(x => x.IdDepartamento == id);
        }

        public void InsertarDepartamento(int id, string nombre,string localidad) {

            Departamento departamento = new Departamento();

            departamento.IdDepartamento = id;
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;

            this.context.Departamentos.Add(departamento);
            this.context.SaveChanges();
        }

        public void UpdateDepartamento(int id,string nombre,string localidad) {

            Departamento departamento = this.FindDepartamento(id);

            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            this.context.SaveChanges();
        }

        public void DeleteDepartamento(int id) {

            Departamento departamento = this.FindDepartamento(id);
            this.context.Departamentos.Remove(departamento);
            this.context.SaveChanges();
        }
    }
}
