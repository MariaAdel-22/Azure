using MvcWebJobsEjercicio.Data;
using MvcWebJobsEjercicio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcWebJobsEjercicio.Repositories
{
    class RepositoryChollometro
    {
        private ChollometroContext context;

        public RepositoryChollometro(ChollometroContext context) {

            this.context = context;
        }

        public void PopulateChollos() {

            //De esta forma accedemos a la página web cuando no nos permite el acceso, como si fuese un navegador
            string url = @"https://www.chollometro.com/rss";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = @"text/html, application/xhtml+xml, */*";
            request.Referer = @"https://www.chollometro.com/";
            request.Headers.Add("Accept-Language", "es-ES");
            request.UserAgent = @"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)";
            request.Host = @"www.chollometro.com";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string xmlChollos;

            using (StreamReader reader = new StreamReader(response.GetResponseStream())) {

                xmlChollos = reader.ReadToEnd();
            }

            XDocument document = XDocument.Parse(xmlChollos);

            var consulta = from datos in document.Descendants("item") select datos;

            List<Chollo> chollosweb = new List<Chollo>();
            int idchollo = this.GetMaxIdChollo();

            foreach (var elem in consulta) {

                Chollo chollo = new Chollo
                {

                    IdChollo = idchollo,
                    Titulo = elem.Element("title").Value,
                    Descripcion = elem.Element("description").Value,
                    Link = elem.Element("link").Value,
                    Fecha = DateTime.UtcNow
                };

                chollosweb.Add(chollo);
                idchollo += 1;
            }

            //insertamos los chollos en la bbdd

            foreach (Chollo chollo in chollosweb) {

                this.context.Chollos.Add(chollo);
            }
            this.context.SaveChanges();
        }

        private int GetMaxIdChollo() {

            if (this.context.Chollos.Count() == 0)
            {

                return 1;
            }
            else {

                return this.context.Chollos.Max(z => z.IdChollo) + 1;
            }
        }
    }
}
