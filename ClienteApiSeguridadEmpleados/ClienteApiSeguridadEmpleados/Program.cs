using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClienteApiSeguridadEmpleados
{
    class Program
    {

        static async Task<string> GetToken() {

            string url = "https://apiempleadosoauth2022maem.azurewebsites.net/";
            MediaTypeWithQualityHeaderValue Header = new MediaTypeWithQualityHeaderValue("application/json");

            Console.WriteLine("Hello World!");

            //PRIMERA PETICION PARA RECUPERAR EL TOKEN DEBEMOS ENVIAR EL LOGINMODEL
            LoginModel model = new LoginModel();

            Console.WriteLine("Usuario");
            model.UserName = Console.ReadLine();

            Console.WriteLine("Password");
            model.Password = int.Parse(Console.ReadLine());

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(url);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(Header);

                //CONVERTIMOS EL MODEL A JSON
                string jsonmodel = JsonConvert.SerializeObject(model);

                //CREAMOS UN CONTENT PARA ENVIAR EL MODELO EN LA PETICION
                StringContent content = new StringContent(jsonmodel, Encoding.UTF8, "application/json");

                string request = "/auth/login";

                HttpResponseMessage response = await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {

                    string data = await response.Content.ReadAsStringAsync();

                    JObject objeto = JObject.Parse(data);

                    string token = objeto.GetValue("response").ToString();

                   return token;
                }
                else {

                    return "Peticion Incorrecta";
                }
 
            }
        }

        public static async Task<string> GetEmpleados(string token) {

            string url = "https://apiempleadosoauth2022maem.azurewebsites.net/";
            MediaTypeWithQualityHeaderValue Header = new MediaTypeWithQualityHeaderValue("application/json");

            using (HttpClient client= new HttpClient()) {

                client.BaseAddress = new Uri(url);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(Header);

                //TENEMOS QUE AÑADIR EN REQUEST HEADER LA CLAVE AUTHORIZATION bearer TOKEN
                client.DefaultRequestHeaders.Add("Authorization","bearer "+token);

                string request = "/api/empleados";
                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {

                    string data = await response.Content.ReadAsStringAsync();
                    return data;
                }
                else {

                    return "Algo ha ocurrido";
                }
            }

        }

        static void Main(string[] args)
        {
            string token = GetToken().Result;

            Console.WriteLine(token);
            Console.WriteLine("------------------------------------");

            string jsonempleados = GetEmpleados(token).Result;

            Console.WriteLine(jsonempleados);
        }
    }
}
