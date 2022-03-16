using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Models
{
    [Table("GENEROS")]
    public class Genero
    {
        [Key]
        [Column("idGenero")]
        public int IdGenero { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }
    }
}
