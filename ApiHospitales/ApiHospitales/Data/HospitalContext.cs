using ApiHospitales.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHospitales.Data
{
    public class HospitalContext:DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext>options):base(options) { 
        
        }

        public DbSet<Hospital> Hospitales { get; set; }
    }
}
