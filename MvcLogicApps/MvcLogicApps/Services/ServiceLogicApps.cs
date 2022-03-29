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

            string urlTabla = "https://prod-109.westeurope.logic.azure.com:443/workflows/f6628bd954df42158e231e0622282dfc/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=dB7iAHa4RnRhtr72HxATmXE0ZGvStlzVQysS4EmUgMM";

            using (HttpClient client = new HttpClient()) {

                var modelNumero = new
                {
                    Numero = numero
                };

                var json = JsonConvert.SerializeObject(modelNumero);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlTabla, content);

                if (response.IsSuccessStatusCode)
                {

                    List<Tabla> tabla = await response.Content.ReadAsAsync<List<Tabla>>();

                    return tabla;
                }
                else {

                    return null;
                }
            }

        }

        public async Task InsertarDoctorAsync(Doctor doctor) {

            string urlFlowInsert = "https://prod-171.westeurope.logic.azure.com:443/workflows/07c75c0afdb146aa8e120d1e04d4c44b/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=G2QL93ar2AjRWQY0DTg8pdFKwSZVaxkn29qw0ACu7uc";

            using (HttpClient client = new HttpClient()) {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(doctor);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(urlFlowInsert, content);

            }
        
        }

        public async Task<List<Doctor>> GetDoctoresAsync() {

            string UrlFlowDoctores = "https://prod-152.westeurope.logic.azure.com:443/workflows/b4bd14bc44434f1ead68f1903382a8a4/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=f1SVOAfupERTsQv2bv3kub_3MeqROb7FnTkwICbC4LI";

            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                HttpResponseMessage response = await client.PostAsync(UrlFlowDoctores, null);

                if (response.IsSuccessStatusCode)
                {

                    List<Doctor> doctores = await response.Content.ReadAsAsync<List<Doctor>>();

                    return doctores;
                }
                else {

                    return null;
                }

            }
        }

        public async Task<Doctor> FindDoctorAsync(string idDoctor) {

            string urlFlowDetail = "https://prod-14.westeurope.logic.azure.com:443/workflows/a9c87cb3ab574e9faf843e7225caf47c/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=VyxgyYRzyMfzowRIVuLlFL0LHUrQz3aDRmauHHx90wE";

            using (HttpClient client = new HttpClient()) {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                var modelId = new { IdDoctor = idDoctor };

                string json = JsonConvert.SerializeObject(modelId);

                StringContent content = new StringContent(json,Encoding.UTF8,"application/json");

                HttpResponseMessage response = await client.PostAsync(urlFlowDetail, content);

                if (response.IsSuccessStatusCode)
                {

                    Doctor doctor = await response.Content.ReadAsAsync<Doctor>();

                    return doctor;
                }
                else {

                    return null;
                }
            }
        }

        public async Task<string> AnalizarComentariosAsync(string comentario) {

            string UrlFlowSentimientos = "https://prod-115.westeurope.logic.azure.com:443/workflows/0fd4b6f3545446ffa811720b1780c7ac/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=ve-zoifU0960MKmQBcya3I-X9Szrgp_fM3Dx4CDf_kw";

            using (HttpClient client= new HttpClient()) {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                var modelComentario = new { Comentario = comentario };

                string json = JsonConvert.SerializeObject(modelComentario);

                StringContent content = new StringContent(json, Encoding.UTF8,"application/json");

                HttpResponseMessage response = await client.PostAsync(UrlFlowSentimientos, content);

                if (response.IsSuccessStatusCode)
                {

                    string data = await response.Content.ReadAsStringAsync();

                    return data;
                }
                else {

                    return "Algo está fallando...";
                }
            }
        }
    }
}
