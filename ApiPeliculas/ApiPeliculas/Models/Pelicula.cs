using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Models
{
    [Table("PELICULA")]
    public class Pelicula
    {
        [Key]
        [Column("idPelicula")]
        public int IdPelicula { get; set; }

        [Column("idDistribuidor")]
        public int IdDistribuidor { get; set; }

        [Column("idGenero")]
        public int IdGenero { get; set; }

        [Column("titulo")]
        public string Titulo { get; set; }

        [Column("idNacionalidad")]
        public int IdNacionalidad { get; set; }

        [Column("argumento")]
        public string Argumento { get; set; }

        [Column("foto")]
        public string Foto { get; set; }

        [Column("fechaEstreno")]
        public string FechaEstreno { get; set; }

        [Column("actores")]
        public string Actores { get; set; }

        [Column("duracion")]
        public int Duracion { get; set; }

        [Column("precio")]
        public int Precio { get; set; }
    }
}
