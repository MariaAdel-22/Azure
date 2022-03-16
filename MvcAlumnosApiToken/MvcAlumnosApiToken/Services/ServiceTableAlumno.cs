using Microsoft.Azure.Cosmos.Table;
using MvcAlumnosApiToken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAlumnosApiToken.Services
{
    public class ServiceTableAlumno
    {
        private string UrlTableAlumno;

        public ServiceTableAlumno(string urltablealumnos) {

            this.UrlTableAlumno = urltablealumnos;
        }

        public async Task<List<Alumno>> GetAlumnosAsync(string token) {

            //PARA CONECTARNOS DE ESTA FORMA NECESITAMOS CREDENCIALES CON EL TOKEN
            StorageCredentials credentials = new StorageCredentials(token);

            //Necesitamos un table client que necesita la url de acceso de la tabla y también recibir las credenciales
            CloudTableClient tableClient = new CloudTableClient(new Uri(this.UrlTableAlumno), credentials);

            CloudTable tablaAlumnos = tableClient.GetTableReference("tablaalumnos");

            TableQuery<Alumno> query = new TableQuery<Alumno>();


            var results = await tablaAlumnos.ExecuteQuerySegmentedAsync(query, null);

            return results.ToList();
        }
    }
}
