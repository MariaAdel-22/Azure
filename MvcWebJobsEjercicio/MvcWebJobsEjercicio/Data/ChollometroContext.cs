using Microsoft.EntityFrameworkCore;
using MvcWebJobsEjercicio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcWebJobsEjercicio.Data
{
    class ChollometroContext:DbContext
    {
        public ChollometroContext(DbContextOptions<ChollometroContext>options):base(options) { 
        
        }

        public DbSet<Chollo> Chollos { get; set; }
    }
}
