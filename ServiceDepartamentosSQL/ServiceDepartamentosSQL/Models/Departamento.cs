using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDepartamentosSQL.Models
{
    [DataContract]
    [Table("DEPT")]
    public class Departamento
    {
        [Key]
        [Column("DEPT_NO")]
        [DataMember]
        public int IdDepartamento { get; set; }

        [Column("DNOMBRE")]
        [DataMember]
        public string Nombre { get; set; }

        [Column("LOC")]
        [DataMember]
        public string Localidad { get; set; }
    }
}
