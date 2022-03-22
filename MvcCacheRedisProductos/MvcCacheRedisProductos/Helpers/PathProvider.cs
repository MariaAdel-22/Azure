using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCacheRedisProductos.Helpers
{
    public enum Folders {
        Images = 0,
        Documents=1,
        Temporal =2
    }

    public class PathProvider
    {
        private IWebHostEnvironment _env;

        public PathProvider(IWebHostEnvironment _env) {

            this._env = _env;
        }

        public string MapPath(string fileName,Folders folder) {

            string carpeta = "";

            if (folder == Folders.Images) {

                carpeta = "images";

            } else if (folder == Folders.Documents) {

                carpeta = "documents";

            } else if (folder == Folders.Temporal) {

                carpeta = "temp";
            }

            string path = Path.Combine(this._env.WebRootPath, carpeta, fileName);

            return path;
        }
    }
}
