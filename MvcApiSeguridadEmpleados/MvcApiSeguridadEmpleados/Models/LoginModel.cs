using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiSeguridadEmpleados.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public int Password { get; set; }
    }
}
