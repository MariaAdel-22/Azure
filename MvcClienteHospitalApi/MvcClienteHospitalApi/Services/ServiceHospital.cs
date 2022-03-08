using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using MvcClienteHospitalApi.Models;
using System.Net.Http;

namespace MvcClienteHospitalApi.Services
{
    public class ServiceHospital
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceHospital() {

            //Metemos la URL SIN LA PETICIÓN

            this.UrlApi = "https://apihospitales2022maem.azurewebsites.net";

            //Le decimos que la cabecera del servicio será del tipo json
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        //TODOS LOS METODOS SON ASÍNCRONOS
        public async Task<List<Hospital>> GetHospitalesAsync() {

            using (HttpClient client =new  HttpClient()) {

                string request = "/api/hospitales";

                //INDICAMOS EL DOMINIO DONDE ESTÁ ALOJADO EL SERVICIO
                client.BaseAddress = new Uri(this.UrlApi);

                //LIMPIAMOS LAS CABECERAS PARA CADA PETICIÓN
                client.DefaultRequestHeaders.Clear();

                //AÑADIMOS EL FORMATO QUE DESEAMOS CONSUMIR
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                //TODA PETICIÓN (INDEPENDIENTEMENTE DEL MÉTODO) SIEMPRE DEVUELVE UNA RESPUESTA DEL SERVIDOR
                //SI ES UNA PETICIÓN GET EN LA RESPUESTA ESTARÁN LOS DATOS SIEMPRE QUE SEA CORRECTA LA PETICIÓN
                HttpResponseMessage response = await client.GetAsync(request);

                //COMPROBAMOS SI LA RESPUESTA ES CORRECTA
                if (response.IsSuccessStatusCode)
                {
                    //EN LA PROPIEDAD CONTENT DE RESPONSE SERÁ DÓNDE TENGAMOS LOS DATOS.
                    //DICHA PROPIEDAD PODEMOS LEERLA DE VARIAS FORMAS. PODRÍAMOS LEER LA PROPIEDAD COMO STRING Y DESERIALIZARLA A OBJETO
                    //TAMBIÉN PODRÍAMOS DESERIALIZAR DIRECTAMENTE CON EL MÉTODO ReadAsAsync<TYPE>

                    List<Hospital> hospitales = await response.Content.ReadAsAsync<List<Hospital>>();

                    //SI VAMOS A PERSONALIZAR LAS PROPIEDADES DEBEMOS HACERLO CON NEWTONSOFT.JSON

                    return hospitales;
                }
                else {

                    return null;
                }
            }
        }
    }
}
