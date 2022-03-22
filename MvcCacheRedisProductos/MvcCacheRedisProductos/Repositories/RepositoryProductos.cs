using MvcCacheRedisProductos.Helpers;
using MvcCacheRedisProductos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCacheRedisProductos.Repositories
{

    public class RepositoryProductos
    {
        private XDocument document;

        public RepositoryProductos(PathProvider provider) {

            string path = provider.MapPath("productos.xml", Folders.Documents);

            this.document = XDocument.Load(path);
        }

        public List<Producto> GetProductos() {

            var consulta = from datos in this.document.Descendants("producto")
                           select new Producto {

                               IdProducto = int.Parse(datos.Element("idproducto").Value),
                               Nombre = datos.Element("nombre").Value,
                               Descripcion = datos.Element("descripcion").Value,
                               Imagen = datos.Element("imagen").Value,
                               Precio = int.Parse(datos.Element("precio").Value)
                           };

            return consulta.ToList();
        }

        public Producto FindProducto(int id) {

            return this.GetProductos().FirstOrDefault(x => x.IdProducto == id);
        }
    }
}
