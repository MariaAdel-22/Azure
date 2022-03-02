using CochesNuGet;
using System;
using System.Collections.Generic;

namespace PruebaCochesNuget
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Coches Nuget!!");
            Garaje g = new Garaje();

            List<Coche> coches = g.GetCoches();

            foreach (Coche car in coches) {

                Console.WriteLine(car.Marca+" "+car.Modelo);
            }
            Console.WriteLine("Pulse ENTER para finalizar");
            Console.ReadLine();
        }
    }
}
