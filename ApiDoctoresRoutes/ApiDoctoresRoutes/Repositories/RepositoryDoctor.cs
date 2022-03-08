using ApiDoctoresRoutes.Data;
using ApiDoctoresRoutes.Models;
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
    }
}
