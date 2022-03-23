using MClienteCacheRedis;
using System;
using System.Collections.Generic;

namespace ClienteCacheRedis
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceCacheRedis service = new ServiceCacheRedis();

            string fin = "y";

            while(fin.ToLower() != "n"){

                List<Producto> favoritos = service.GetFavoriteProducts();

                if (favoritos == null)
                {

                    Console.WriteLine("No existen favoritos");
                }
                else {

                    int i = 1;

                    foreach (Producto producto in favoritos) {

                        Console.WriteLine(i + " .- " + producto.Nombre);
                        i += 1;
                    }

                    Console.WriteLine("------------------------------------------");
                }

                Console.WriteLine("¿Desea cargar más favoritos? (y/n)");
                fin = Console.ReadLine();
            }

            Console.WriteLine("Fin del programa");
        }
    }
}
