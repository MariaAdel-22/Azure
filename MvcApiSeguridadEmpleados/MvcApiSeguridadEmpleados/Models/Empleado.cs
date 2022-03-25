using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiSeguridadEmpleados.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Apellido { get; set; }
        public string Oficio { get; set; }
        public int Director { get; set; }
        public int Salario { get; set; }
        public int IdDepartamento { get; set; }
    }
}
