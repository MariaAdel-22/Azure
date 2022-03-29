using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAlumnoPractica.Data;
using WebApiAlumnoPractica.Models;

namespace WebApiAlumnoPractica.Repositories
{
    public class RepositoryAlumno
    {
        private AlumnoContext context;

        public RepositoryAlumno(AlumnoContext context) {

            this.context = context;
        }

        public List<Alumno> GetAlumnos() {

            return this.context.Alumnos.ToList();
        }

        public Alumno FindAlumno(string nombre,string apellidos) {

            return this.context.Alumnos.FirstOrDefault(x => x.Nombre == nombre && x.Apellidos == apellidos);
        }

        public Alumno FinIdAlumno(int id) {

            return this.context.Alumnos.FirstOrDefault(x => x.IdAlumno == id);
        }

        public List<Alumno> GetAlumnosCurso(string curso) {

            return this.context.Alumnos.Where(x => x.Curso == curso).ToList();
        }

        private int GetMaxId() {

            if (this.context.Alumnos.Count() == 0)
            {

                return 1;
            }
            else {

              return  this.context.Alumnos.Max(x => x.IdAlumno) + 1;
            }
        }

        public void InsertarAlumno(string Curso, string Nombre, string Apellidos, int Nota)
        {

            Alumno al = new Alumno
            {
                IdAlumno = this.GetMaxId(), Curso=Curso,Nombre=Nombre,Apellidos=Apellidos,Nota=Nota
            };

            this.context.Alumnos.Add(al);
            this.context.SaveChanges();
        }

        public void ModificarAlumno(int idAlumno, string Curso, string Nombre, string Apellidos, int Nota) {

            Alumno al = this.FinIdAlumno(idAlumno);

            if (al != null) {

                al.Curso = Curso;
                al.Nombre = Nombre;
                al.Apellidos = Apellidos;
                al.Nota = Nota;
            }

            this.context.SaveChanges();
        }

        public void DeleteAlumno(int idAlumno) {

            Alumno al = this.FinIdAlumno(idAlumno);

            this.context.Alumnos.Remove(al);
        }
    }
}
