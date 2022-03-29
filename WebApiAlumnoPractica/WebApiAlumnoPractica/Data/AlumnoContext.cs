using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAlumnoPractica.Models;

namespace WebApiAlumnoPractica.Data
{
    public class AlumnoContext:DbContext
    {
        public AlumnoContext
        (DbContextOptions<AlumnoContext> options) : base(options) { }
        public DbSet<Alumno> Alumnos { get; set; }

    }
}
