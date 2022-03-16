using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAlumnosSAS.Services
{
    public class ApiAlumnossas
    {
        private CloudTable tableAlumnos;

        public ApiAlumnossas(string keys) {

            CloudStorageAccount account = CloudStorageAccount.Parse(keys);

            CloudTableClient tableClient = account.CreateCloudTableClient();

            this.tableAlumnos = tableClient.GetTableReference("alumnos");

        }

        //necesitamos un metodo que recibira el curso para filtrar y generar un token de acceso
        public string GetToken(string curso) {

            //Necesitamos crear una politica de acceso que indicará el tiempo y los permisos sobre los objetos que tendrá el token
            SharedAccessTablePolicy policy = new SharedAccessTablePolicy
            {
                SharedAccessExpiryTime=DateTime.UtcNow.AddMinutes(10),
                Permissions=SharedAccessTablePermissions.Query 
                // le añadimos un OR para que pueda realizar más de una acción
                | SharedAccessTablePermissions.Add
            };

            //se genera un token con estos permisos de policy
            //Debemos indicar el nombre de la politica para el token y si deseamos filtrar elementos de la tabla por ROWKEY
            //o por PARTITION KEY
            string token = this.tableAlumnos.GetSharedAccessSignature(policy, null, curso, null, curso, null);

            return token;
        }
    }
}
