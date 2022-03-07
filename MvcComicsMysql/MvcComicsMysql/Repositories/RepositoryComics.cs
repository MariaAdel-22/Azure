using MvcComicsMysql.Data;
using MvcComicsMysql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcComicsMysql.Repositories
{
    public class RepositoryComics
    {
        private ComicsContext context;

        public RepositoryComics(ComicsContext context) {

            this.context = context;
        }

        public List<Comic> GetComics() {

            return this.context.Comics.ToList();
        }
    }
}
