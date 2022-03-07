using Microsoft.EntityFrameworkCore;
using MvcComicsMysql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcComicsMysql.Data
{
    public class ComicsContext: DbContext 
    {
        public ComicsContext(DbContextOptions<ComicsContext> options):base(options) { 
        }

        public DbSet<Comic> Comics { get; set; }
    }
}
