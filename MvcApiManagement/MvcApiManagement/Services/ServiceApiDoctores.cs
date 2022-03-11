using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Web;
using System.Collections.Specialized;
using System.Net.Http;
using MvcApiManagement.Models;

namespace MvcApiManagement.Services
{
    public class ServiceApiDoctores
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;
        private NameValueCollection queryString;

        public ServiceApiDoctores(string url) {

            this.UrlApi = url;
            this.queryString = HttpUtility.ParseQueryString(string.Empty);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");

        }

        private async Task<T> CallApiAsync<T>(string request) {

            using (HttpClient client= new HttpClient()) {

                request = request + "?" + this.queryString;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");

                //HEADER NAME, LA CLAVE DE LA PK
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "5b8245e900524bde94cfead7a7842715");

                HttpResponseMessage response = await client.GetAsync(this.UrlApi + request);

                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else {

                    return default(T);
                }
            }
        }

        public async Task<List<Doctor>> GetDoctoresAsync() {

            string request = "/api/doctores";

            List<Doctor> doctores = await this.CallApiAsync<List<Doctor>>(request);

            return doctores;
        }
    }
}
