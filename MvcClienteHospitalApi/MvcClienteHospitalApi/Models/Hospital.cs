using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClienteHospitalApi.Models
{
    public class Hospital
    {
        public int idHospital { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public int Camas { get; set; }
    }
}
