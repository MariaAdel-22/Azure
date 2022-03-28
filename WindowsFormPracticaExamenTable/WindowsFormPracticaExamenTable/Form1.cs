using Microsoft.Azure.Cosmos.Table;
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
using WindowsFormPracticaExamenTable.Models;

namespace WindowsFormPracticaExamenTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async Task btnMigrar_Click(object sender, EventArgs e)
        {

            //NECESITAMOS LAS KEYS DE AZURE STORAGE
            string azureKeys = "DefaultEndpointsProtocol=https;AccountName=storagetajamarmaem;AccountKey=X+T05BQ3uCkfTtegk5MwNzv/ZPGk1O3UfpZ/Xx0+gP5Hs6WO3WH7+Ic2fkYnf7iQjCG32L5m3cP8JRxF1c68CA==;EndpointSuffix=core.windows.net";
            CloudStorageAccount account =
                CloudStorageAccount.Parse(azureKeys);
            CloudTableClient tableClient = account.CreateCloudTableClient();
            CloudTable tabla = tableClient.GetTableReference("alumnos");
            await tabla.CreateIfNotExistsAsync();
            //RECUPERAMOS EL DOCUMENTO INCRUSTADO MEDIANTE
            //EL NOMBRE DEL ASSEMBLY (namespace) Y CARPETA.FICHERO
            string recurso = "WindowsFormPracticaExamenTable.alumnos_tables.xml";
            Stream stream =
                this.GetType().Assembly.GetManifestResourceStream(recurso);
            XDocument document = XDocument.Load(stream);
            var consulta = from datos in document.Descendants("alumno")
                           select new Alumno
                           {
                               idAlumno = datos.Element("idalumno").Value,
                               curso = datos.Element("curso").Value,
                               Nombre = datos.Element("nombre").Value,
                               Apellidos = datos.Element("apellidos").Value,
                               Nota = int.Parse(datos.Element("nota").Value)
                           };
            //RECORREMOS LOS ALUMNOS DE LA CONSULTA Y 
            //CREAMOS UNA OPERACION INSERT PARA AZURE STORAGE TABLES
            foreach (Alumno alumno in consulta)
            {
                TableOperation insert = TableOperation.Insert(alumno);
                await tabla.ExecuteAsync(insert);
            }
        }
    }
}
