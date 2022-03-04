using MvcCoreChollometro.Data;
using MvcCoreChollometro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreChollometro.Repositories
{
    public class RepositoryChollometro
    {
        private ChollometroContext context;

        public RepositoryChollometro(ChollometroContext context) {

            this.context = context;
        }

        public List<Chollo> GetChollos() {

            var consulta = from datos in this.context.Chollos orderby datos.IdChollo descending select datos;

            return consulta.ToList();
        }
    }
}
