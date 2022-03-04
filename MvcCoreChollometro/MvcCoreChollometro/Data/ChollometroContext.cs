using Microsoft.EntityFrameworkCore;
using MvcCoreChollometro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreChollometro.Data
{
    public class ChollometroContext:DbContext
    {
        public ChollometroContext(DbContextOptions<ChollometroContext> options) : base(options)
        {

        }

        public DbSet<Chollo> Chollos { get; set; }
    }
}
