using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcAlumnosApiToken.Services
{
    public class ServiceApiToken
    {
        private string UrlApi;
        public ServiceApiToken(string urlapi) {

            this.UrlApi = urlapi;
        }

        public async Task<string> GetTokenAsync(string curso) {

            string request = "/api/alumnoskeys/"+curso;

            using (WebClient client = new WebClient()) {

                client.Headers["content-type"] = "application/json";

                Uri uri = new Uri(this.UrlApi + request);

                string content = await client.DownloadStringTaskAsync(uri);
                JObject data = JObject.Parse(content);

                string token = data.GetValue("token").ToString();

                return token;
            }
        }

    }
}
