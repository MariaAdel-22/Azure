using ApiDoctoresRoutes.Data;
using NuGetDoctores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDoctoresRoutes.Repositories
{
    public class RepositoryDoctor
    {
        private DoctoresContext context;

        public RepositoryDoctor(DoctoresContext context) {

            this.context = context;
        }

        public List<Doctor> GetDoctores() {

            return this.context.Doctores.ToList();
        }

        public Doctor GetDoctor(string id) {

            return this.context.Doctores.SingleOrDefault(x => x.DoctorCod == id);
        }

        public List<string> GetEspecialidades() {

            var consulta = (from datos in this.context.Doctores select datos.Especialidad).Distinct();
            return consulta.ToList();
        }

        public List<Doctor> GetDoctoresEspecialidades(string especialidad) {

            var consulta = from datos in this.context.Doctores where datos.Especialidad == especialidad select datos;
            return consulta.ToList();
        }

        public List<Doctor> GetDoctores(int salario, string especialidad)
        {

            var consulta = from datos in this.context.Doctores
            where datos.Salario >= salario && datos.Especialidad == especialidad select datos;

            return consulta.ToList();
        }
    }
}
