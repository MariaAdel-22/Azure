using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValidarInicioSesionToken.Models
{
    public class Alumno
    {
        public int idAlumno { get; set; }
        public string Curso { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Nota { get; set; }
    }
}
