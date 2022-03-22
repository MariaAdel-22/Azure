using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCacheRedisProductos.Helpers
{
    public static class CacheRedisMultiplexer
    {
        public static Lazy<ConnectionMultiplexer> CreateConnection = new Lazy<ConnectionMultiplexer>(() =>
        {

            return ConnectionMultiplexer.Connect("productoscachemaem.redis.cache.windows.net:6380,password=YKhthHCilDaOpi812g8JNCb7YCLIY4UUwAzCaLP2uXk=,ssl=True,abortConnect=False");
        });

        public static ConnectionMultiplexer GetConnection
        {
            get {

                return CreateConnection.Value;
            }
        }
    }
}
