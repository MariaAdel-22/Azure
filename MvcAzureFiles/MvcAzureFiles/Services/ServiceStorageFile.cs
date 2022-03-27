using Azure.Storage.Files.Shares;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAzureFiles.Services
{
    public class ServiceStorageFile
    {
        //TODO FUNCIONA A PARTIR DE UN RECURSO COMPARTIDO.
        //NECESITAMOS RECUPERAR LOS ELEMENTOS A PARTIR DE NUESTRO Shared
        private ShareDirectoryClient Root;

        public ServiceStorageFile(string keys)
        {
            //LOS ACCESOS A STORAGE SE PUEDEN REALIZAR DE MULTIPLES 
            //FORMAS.
            //UNA DE ELLAS ES UTILIZANDO UN CLIENTE Y UNAS ACCESS KEYS.
            //LAS CLAVES DE ACCESO PERTENECEN A TODO EL STORAGE Y SE PUEDEN
            //UTILIZAR EN CUALQUIER RECURSO
            ShareClient client = new ShareClient(keys, "ejemplofiles");
            //DENTRO DEL SHARED PODREMOS TENER MULTIPLES DIRECTORIOS.
            //NOSOTROS APUNTAREMOS A ROOT DIRECTAMENTE
            this.Root = client.GetRootDirectoryClient();
        }

        //MOSTRAR TODOS FICHEROS
        public async Task<List<string>> GetFilesAsync()
        {
            List<string> ficheros = new List<string>();
            await foreach (var file in this.Root.GetFilesAndDirectoriesAsync())
            {
                ficheros.Add(file.Name);
            }
            return ficheros;
        }

        //METODO PARA LEER EL CONTENIDO (string) DE UN FILE
        public async Task<string> ReadFileAsync(string fileName)
        {
            ShareFileClient file = this.Root.GetFileClient(fileName);
            var data = await file.DownloadAsync();
            Stream stream = data.Value.Content;
            using (StreamReader reader = new StreamReader(stream))
            {
                string content = await reader.ReadToEndAsync();
                return content;
            }
        }

        //METODO PARA SUBIR FICHERO A AZURE
        public async Task UploadFileAsync(string fileName, Stream stream)
        {
            ShareFileClient file = this.Root.GetFileClient(fileName);
            await file.CreateAsync(stream.Length);
            await file.UploadAsync(stream);
        }

        //METODO PARA ELIMINAR FILES
        public async Task DeleteFileAsync(string fileName)
        {
            ShareFileClient file = this.Root.GetFileClient(fileName);
            await file.DeleteAsync();
        }

    }
}
