using ServiceDepartamentosSQL.Data;
using ServiceDepartamentosSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDepartamentosSQL.Repositories
{
    public class RepositoryDepartamentos
    {
        DepartamentosContext context;

        public RepositoryDepartamentos() {

            this.context = new DepartamentosContext();
        }

        public List<Departamento> GetDepartamentos() {

            return this.context.Departamentos.ToList();
        }

        public Departamento BuscarDepartamento(int id) {

            return this.context.Departamentos.SingleOrDefault(x => x.IdDepartamento == id);
        }
    }
}
