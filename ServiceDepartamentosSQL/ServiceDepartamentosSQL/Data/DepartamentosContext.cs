using ServiceDepartamentosSQL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDepartamentosSQL.Data
{
    public class DepartamentosContext:DbContext
    {

        public DepartamentosContext() : base("name=cadenahospital") { 
        
        }

        public DbSet<Departamento> Departamentos { get; set; }
    }
}
