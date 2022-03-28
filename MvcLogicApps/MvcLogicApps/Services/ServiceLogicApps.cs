using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using MvcLogicApps.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcLogicApps.Services
{
    public class ServiceLogicApps
    {
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceLogicApps() {

            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task SendMail(string email,string subject,string body) {

            string urlMail = "https://prod-109.westeurope.logic.azure.com:443/workflows/406f36c4a7654e4e9b2ffd248f397309/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=lrUkVGBkX0sT6aUAIkzw4EuFiCYEoQCUTBeiU7khrg0";

            using (HttpClient client = new HttpClient()) {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                EmailModel emailModel = new EmailModel
                {
                    Email=email,Subject=subject,Body=body
                };

                //CONVERTIMOS EL MODEL A JSON
                string json = JsonConvert.SerializeObject(emailModel);

                //LAS PETICIONES POST Y RECIBE LA INFORMACION EN JSON MEDIANTE STRINGCONTENT
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(urlMail,content);


            }
        }

        public async Task<string> SumarNumerosAsync(int numero1,int numero2) {

            string urlFlowSuma = "https://prod-214.westeurope.logic.azure.com:443/workflows/2d73d6ad6b9d4c74afacab15bd8201a3/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=kHw6DjETGXD4cyy-HzlTZDARJqaM-MDI6ZzWqAw7Xl4";

            using (HttpClient client = new HttpClient()) {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                var sumaModel = new
                {

                    Numero1 = numero1,
                    Numero2 = numero2
                };

                var json = JsonConvert.SerializeObject(sumaModel);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(urlFlowSuma, content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    return "La suma es: " + data;
                }
                else {

                    return null;
                }
            }
        }

        public async Task<List<Tabla>> TablaMultiplicarAsync(int numero) { 
        

        }
    }
}
