using ApiHospitales.Data;
using ApiHospitales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHospitales.Repositories
{
    public class RepositoryHospital
    {
        private HospitalContext context;

        public RepositoryHospital(HospitalContext context) {

            this.context = context;
        }

        public List<Hospital> GetHospitales() {

            return this.context.Hospitales.ToList();
        }

        public Hospital FindHospital(int id) {

            return this.context.Hospitales.FirstOrDefault(z => z.IdHospital == id);
        }
    }
}
