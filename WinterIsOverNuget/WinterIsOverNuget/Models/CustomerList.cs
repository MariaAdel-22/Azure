using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterIsOverNuget.Models
{
    public class CustomerList
    {
        //Este array es el que contiene todos los customers en el JSON por eso hay que mapearlo también
        [JsonProperty("value")]
        public List<Customer> Customers { get; set; }
    }
}
