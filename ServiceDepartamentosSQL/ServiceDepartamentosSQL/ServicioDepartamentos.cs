using ServiceDepartamentosSQL.Models;
using ServiceDepartamentosSQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDepartamentosSQL
{
    public class ServicioDepartamentos : IServicioDepartamentos
    {
        RepositoryDepartamentos repo;

        public ServicioDepartamentos() {
            this.repo = new RepositoryDepartamentos();
        }

        public Departamento BuscarDepartamento(int id)
        {
            return this.repo.BuscarDepartamento(id);
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.repo.GetDepartamentos();
        }
    }
}
