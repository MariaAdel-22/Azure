using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClientePeliculas.Models
{
    public class Pelicula
    {
        public int IdPelicula { get; set; }

        public int IdDistribuidor { get; set; }

        public int IdGenero { get; set; }

        public string Titulo { get; set; }

        public int IdNacionalidad { get; set; }

        public string Argumento { get; set; }

        public string Foto { get; set; }

        public string FechaEstreno { get; set; }

        public string Actores { get; set; }

        public int Duracion { get; set; }

        public int Precio { get; set; }
    }
}
