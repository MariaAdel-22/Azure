using Microsoft.Azure.Cosmos.Table;
using MigracionXMLAlumnos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MigracionXMLAlumnos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Necesitamos las Keys de Azure Storage
            string azurekeys = "DefaultEndpointsProtocol=https;AccountName=storagetajamarmaem;AccountKey=X+T05BQ3uCkfTtegk5MwNzv/ZPGk1O3UfpZ/Xx0+gP5Hs6WO3WH7+Ic2fkYnf7iQjCG32L5m3cP8JRxF1c68CA==;EndpointSuffix=core.windows.net";
            
            CloudStorageAccount account = CloudStorageAccount.Parse(azurekeys);
            CloudTableClient tableClient = account.CreateCloudTableClient();

            CloudTable tabla = tableClient.GetTableReference("tablaalumnos");

            await tabla.CreateIfNotExistsAsync();

            //Recuperamos el documento incrustado mediante el nombre de ASSEMBLY (namespace) y carpeta/fichero
            string recurso = "MigracionXMLAlumnos.alumnos_tables.xml";

            Stream stream = this.GetType().Assembly.GetManifestResourceStream(recurso);

            XDocument document = XDocument.Load(stream);

            var consulta = from datos in document.Descendants("alumno")
                           select new Alumno
                           {
                               idAlumno=datos.Element("idalumno").Value,
                               curso=datos.Element("curso").Value,
                               Nombre=datos.Element("nombre").Value,
                               Apellidos=datos.Element("apellidos").Value,
                               Nota=int.Parse(datos.Element("nota").Value)
                           };

            //Recorremos los alumnos de la consulta y creamos una operación insert para azure storage tables

            foreach (Alumno al in consulta) {

                TableOperation insert = TableOperation.Insert(al);

                await tabla.ExecuteAsync(insert);
            }
        }
    }
}
