using ApiCrudDoctores2022.Data;
using ApiCrudDoctores2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudDoctores2022.Repositories
{
    public class RepositoryDoctor
    {
        private DoctorContext context;

        public RepositoryDoctor(DoctorContext context) {

            this.context = context;
        }

        public List<Doctor> GetDoctores() {

            return this.context.Doctores.ToList();
        }

        public Doctor FindDoctor(string iddoctor) {

            return this.context.Doctores.SingleOrDefault(x=>x.IdDoctor == iddoctor);
        }

        public void CrearDoctor(string idhospital,string apellido,string especialidad,int salario) {

            Doctor doc = new Doctor();

            doc.IdDoctor = this.GetMaxIdDoctor().ToString();
            doc.IdHospital = idhospital;
            doc.Apellido = apellido;
            doc.Especialidad = especialidad;
            doc.Salario = salario;

            this.context.Doctores.Add(doc);
            this.context.SaveChanges();
        }

        public void ModificarDoctor(string iddoctor, string idhospital, string apellido, string especialidad, int salario) {

            Doctor doc = this.FindDoctor(iddoctor);

            if (doc != null) {

                doc.IdHospital = idhospital;
                doc.Apellido = apellido;
                doc.Especialidad = especialidad;
                doc.Salario = salario;

                this.context.SaveChanges();
            }
        }

        public void EliminarDoctor(string iddoctor) {

            Doctor doc = this.FindDoctor(iddoctor);

            this.context.Doctores.Remove(doc);
            this.context.SaveChanges();

        }

        private int GetMaxIdDoctor() {

            if (this.context.Doctores.Count() == 0)
            {

                return 1;
            }
            else {

                return this.context.Doctores.Max(z => int.Parse(z.IdDoctor)) + 1;
            }
        }

        public List<String> GetEspecialidades()
        {
            var consulta = (from datos in this.context.Doctores
                            select datos.Especialidad).Distinct();
            return consulta.ToList();
        }

        public List<Doctor> GetDoctoresEspecialidad(string especialidad)
        {
            var consulta = from datos in this.context.Doctores
                           where datos.Especialidad == especialidad
                           select datos;
            return consulta.ToList();
        }

        public List<Doctor> GetDoctoresEspecialidades(List<string>especialidades)
        {
            var consulta = from datos in this.context.Doctores
                           where especialidades.Contains(datos.Especialidad)
                           select datos;
            return consulta.ToList();
        }

        public void UpdateSalarioDoctores(string especialidad,int incremento) {

            List<Doctor> doctores = this.GetDoctoresEspecialidad(especialidad);

            foreach (Doctor doc in doctores) {

                doc.Salario += incremento;
            }

            this.context.SaveChanges();
        }
    }
}
