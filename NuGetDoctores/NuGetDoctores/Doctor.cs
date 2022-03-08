using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGetDoctores
{
    [Table("DOCTOR")]
    public class Doctor
    {
        [Key]
        [Column("HOSPITAL_COD")]
        public string HospitalCod { get; set; }

        [Column("DOCTOR_NO")]
        public string DoctorCod { get; set; }

        [Column("APELLIDO")]
        public string Apellido { get; set; }

        [Column("ESPECIALIDAD")]
        public string Especialidad { get; set; }

        [Column("SALARIO")]
        public int Salario { get; set; }
    }
}
