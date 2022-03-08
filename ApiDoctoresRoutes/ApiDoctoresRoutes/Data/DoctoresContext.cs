using ApiDoctoresRoutes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDoctoresRoutes.Data
{
    public class DoctoresContext:DbContext
    {
        public DoctoresContext(DbContextOptions<DoctoresContext>options):base(options) { 
        
        }

        public DbSet<Doctor> Doctores { get; set; }
    }
}
