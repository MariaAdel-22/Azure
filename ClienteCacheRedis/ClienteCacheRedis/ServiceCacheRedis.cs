using ClienteCacheRedis;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MClienteCacheRedis
{
    public class ServiceCacheRedis
    {
        private IDatabase database;

        public ServiceCacheRedis() {

            this.database = CacheRedisMultiplexer.GetConnection.GetDatabase();
        }

        //Metodo para almacenar producto en cache

        public void AddProduct(Producto producto) {

            //CACHE REDIS FUNCIONA CON KEY/VALUES UNICAS EN SU INTERIOR
            //SI VOLVEMOS A ACCEDER A LA CLAVE SOBREESCRIBE EL VALOR. RECUPERAMOS LA CLAVE UNICA DE FAVORITOS
            string jsonProductos = this.database.StringGet("favoritos");

            List<Producto> favoritos;

            if (jsonProductos == null)
            {

                //NO HAY PRODUCTOS EN CACHE
                favoritos = new List<Producto>();
            }
            else {

                //TENEMOS FAVORITOS ALMACENADOS Y LOS DESERIALIZAMOS
                //PARA RECUPERARLOS
                favoritos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
            }

            favoritos.Add(producto);
            jsonProductos = JsonConvert.SerializeObject(favoritos);

            //ALMACENAMOS LA CLAVE DENTRO DE CACHE REDIS
            this.database.StringSet("favoritos", jsonProductos);
        }

        //METODO PARA RECUPERAR LOS PRODUCTOS FAVORITOS EN CACHE REDIS
        public List<Producto> GetFavoriteProducts() {

            string jsonProductos = this.database.StringGet("favoritos");

            if (jsonProductos == null)
            {

                return null;
            }
            else {

                List<Producto> favoritos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
                return favoritos;
            }
        }

        //METODO PARA ELIMINAR PRODUCTO FAVORITO
        public void DeleteFavoriteProduct(int idProducto) {

            string jsonProductos = this.database.StringGet("favoritos");

            if (jsonProductos != null) {

                List<Producto> favoritos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);

                //DENTRO DE LA COLECCION BUSCAMOS EL PRODUCTO FAVORITO POR SU ID
                Producto eliminar = favoritos.SingleOrDefault(z => z.IdProducto == idProducto);

                //ELIMINAMOS
                favoritos.Remove(eliminar);

                if (favoritos.Count() == 0)
                {

                    //ELIMINAMOS LA KEY DE AZURE
                    this.database.KeyDelete("favoritos");
                }
                else {

                    jsonProductos = JsonConvert.SerializeObject(favoritos);

                    //AL ALMACENAR ELEMENTOS DENTRO DE UNA KEY PODEMOS INDICAR EL TIEMPO DE ALMACENAMIENTO
                    this.database.StringSet("favoritos", jsonProductos, TimeSpan.FromMinutes(15));
                }
            }
        }

    }
}
