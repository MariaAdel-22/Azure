using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcWebJobsEjercicio.Data;
using MvcWebJobsEjercicio.Repositories;
using System;

namespace MvcWebJobsEjercicio
{
    class Program
    {
        static void Main(string[] args)
        {
            string cadena = @"Data Source=sqladopet.database.windows.net;Initial Catalog=AZUREADOPET;Persist Security Info=True;User ID=adminsql;Password=Admin123";

            //Creamos la inyección de dependencias y montamos el servicio (provider)
            var provider = new ServiceCollection()
                .AddTransient<RepositoryChollometro>().AddDbContext<ChollometroContext>(op => op.UseSqlServer(cadena))
                .BuildServiceProvider();

            //Creamos el repositorio al llamar al servicio con el provider y cargamos/llamamos al método
            RepositoryChollometro repo = provider.GetService<RepositoryChollometro>();

            Console.WriteLine("Pulse en ENTER para iniciar...");
            Console.ReadLine();

            repo.PopulateChollos();

            Console.WriteLine("Proceso terminado");
            Console.ReadLine();

        }
    }
}
