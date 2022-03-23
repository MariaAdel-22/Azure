using Microsoft.Azure.Cosmos;
using MvcCocheCosmosDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCocheCosmosDb.Services
{
    public class ServiceCocheCosmos
    {
        private CosmosClient cosmosClient;
        private Container containerCosmos;

        public ServiceCocheCosmos(CosmosClient client, Container container) {

            this.cosmosClient = client;
            this.containerCosmos = container;
        }

        //METODO PARA CREAR LA BBDD Y EL CONTAINER
        public async Task CreateDatabaseAsync() {

            //DEBEMOS INDICAR LAS PROPIEDADES DEL CONTAINER
            ContainerProperties properties = new ContainerProperties("containercoches", "/id");

            await this.cosmosClient.CreateDatabaseAsync("cochescosmos");
            await this.cosmosClient.GetDatabase("cochescosmos").CreateContainerAsync(properties);

        }

        //METODO PARA CREAR NUEVOS VEHICULOS EN COSMOS
        public async Task AddVehiculoAsync(Vehiculo car) {

            //EN EL MOMENTO DE CREAR UN ITEM (VEHICULO) DEBEMOS INDICAR LA CLASE, EL OBJETO Y EL PARTITION KEY(/id)
            await this.containerCosmos.CreateItemAsync<Vehiculo>(car, new PartitionKey(car.Id));
        }

        //METODO PARA RECUPERAR TODOS LOS ITEMS DE COSMOS
        public async Task<List<Vehiculo>> GetVehiculosAsync() {

            //CREAMOS UNA CONSULTA ITERATOR
            var query = this.containerCosmos.GetItemQueryIterator<Vehiculo>();

            List<Vehiculo> coches = new List<Vehiculo>();

            while (query.HasMoreResults) {

                var results = await query.ReadNextAsync();

                coches.AddRange(results);
            }

            return coches;
        }

        //METODO PARA MODIFICAR UN ITEM 
        public async Task UpdateVehiculoAsync(Vehiculo car) {

            //TENEMOS UN METODO QUE ES UPSERT, ES UNA MEZCLA ENTRE INSERT Y UPDATE. SI LO ENCUENTRA LO MODIFICA, SINO ENTONCES INSERTA
            await this.containerCosmos.UpsertItemAsync<Vehiculo>(car, new PartitionKey(car.Id));
        }

        //METODO PARA BUSCAR VEHICULO POR SU ID
        public async Task<Vehiculo> FindVehiculoAsync(string id) {

            ItemResponse<Vehiculo> response = await this.containerCosmos.ReadItemAsync<Vehiculo>(id,new PartitionKey(id));

            return response.Resource;
        }

        //METODO PARA ELIMINAR UN ITEM
        public async Task DeleteVehiculoAsync(string id) {

            await this.containerCosmos.DeleteItemAsync<Vehiculo>(id, new PartitionKey(id));
        }
    }
}
