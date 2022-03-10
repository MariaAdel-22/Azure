using ApiCrudDoctores2022.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudDoctores2022.Data
{
    public class DoctorContext:DbContext
    {
        public DoctorContext(DbContextOptions<DoctorContext> options):base(options) { 
        
        }

        public DbSet<Doctor> Doctores { get; set; }
    }
}
